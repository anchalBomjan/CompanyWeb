using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApp.API.Data;
using WebApp.API.Models;

namespace WebApp.API.Repositories.IRepository
{
    public class RoleRepository : IRoleRepository
    {

        private readonly DbConnector _dbConnector;
        public RoleRepository(DbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task SeedRolesAsync()
        {
            // Create and open the connection
            using (var dbConnection = _dbConnector.CreateConnection() as SqlConnection)
            {
                if (dbConnection == null)
                {
                    throw new InvalidOperationException("Unable to create a SQL connection.");
                }



                try
                {
                    await dbConnection.OpenAsync(); // Asynchronously open the connection

                    var roleNames = new[]
                    {
                        StaticUserRoles.ADMIN,
                        StaticUserRoles.HR,
                        StaticUserRoles.USER
                    };

                    foreach (var roleName in roleNames)
                    {
                        // Check if the role already exists
                        var roleExists = await dbConnection.QueryFirstOrDefaultAsync<int>(
                            "SELECT COUNT(1) FROM Roles WHERE RoleName = @RoleName",
                            new { RoleName = roleName }
                        );

                        // Insert the role if it does not exist
                        if (roleExists == 0)
                        {
                            await dbConnection.ExecuteAsync(
                                "INSERT INTO Roles (RoleName) VALUES (@RoleName)",
                                new { RoleName = roleName }
                            );
                           
                        }

                    }
                }
                catch (Exception ex)
                {
                    // Handle or log the exception
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw; // Optionally rethrow the exception
                }


              
            }
        }




        public async Task<bool> AssignRoleToUserAsync(string username, string roleName)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));

            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException("Role name cannot be null or empty.", nameof(roleName));

            using (var connection = _dbConnector.CreateConnection() as SqlConnection)
            {
                await connection.OpenAsync(); // Ensure the connection is open

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Retrieve the user ID
                        var userId = await connection.QuerySingleOrDefaultAsync<int>(
                            "SELECT Id FROM Users WHERE Username = @Username",
                            new { Username = username },
                            transaction: transaction
                        );

                        if (userId == 0)
                        {
                            throw new InvalidOperationException("User not found.");
                        }

                        // Retrieve the role ID
                        var roleId = await connection.QuerySingleOrDefaultAsync<int>(
                            "SELECT Id FROM Roles WHERE RoleName = @RoleName",
                            new { RoleName = roleName },
                            transaction: transaction
                        );

                        if (roleId == 0)
                        {
                            throw new InvalidOperationException("Role not found.");
                        }

                        // Assign the role to the user
                        var rowsAffected = await connection.ExecuteAsync(
                            "INSERT INTO UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)",
                            new { UserId = userId, RoleId = roleId },
                            transaction: transaction
                        );

                        // Commit the transaction
                        transaction.Commit();

                        return rowsAffected > 0;
                    }
                    catch
                    {
                        // Rollback the transaction if any error occurs
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

       
        
    }
}

