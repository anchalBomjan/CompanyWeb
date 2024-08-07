using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using WebApp.API.Data;
using WebApp.API.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApp.API.Repositories.IRepository
{
    public class RoleRepository : IRoleRepository
    {

        private readonly DbConnector _dbConnector;
        public RoleRepository(DbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }



        public async Task<List<string>> SeedRolesAsync()
        {
            using (var connection = _dbConnector.CreateConnection() as SqlConnection)



            {
                if (connection == null)
                {
                    throw new InvalidOperationException("Unable to Createa SQL the connnection");
                }
                var messages = new List<string>();


                try
                {
                    await connection.OpenAsync();

                    var roleNames = new[]
                    {
                        StaticUserRoles.ADMIN,
                        StaticUserRoles.HR,
                        StaticUserRoles.USER
                    };
                    foreach (var roleName in roleNames)
                    {
                        var roleExists = await connection.QueryFirstOrDefaultAsync<int>(
                            "SELECT COUNT(1) FROM Roles WHERE RoleName = @RoleName",
                            new { RoleName = roleName }
                        );

                        if (roleExists == 0)
                        {
                            await connection.ExecuteAsync(
                                "INSERT INTO Roles (RoleName) VALUES (@RoleName)",
                                new { RoleName = roleName }
                            );
                            messages.Add($"Role '{roleName}' seeded successfully.");




                        }
                        else
                        {
                            messages.Add($"Role '{roleName}' already exists.");
                        }



                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }

                return messages;
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



        public async Task<List<string>> GetAllRolesAsync()
        {
            using (var connection = _dbConnector.CreateConnection() as SqlConnection)
            {



                if (connection == null)
                {
                    throw new InvalidOperationException("Unable to create a SQL connection.");
                }

                try
                {
                    await connection.OpenAsync();

                    var roles = await connection.QueryAsync<string>(
                        "SELECT RoleName FROM Roles"
                    );

                    return roles.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }


        }

        public async Task<bool> RemoveRoleFromUserRolesAsync(string username, string roleName)
        {
            using (var connection = _dbConnector.CreateConnection() as SqlConnection)
            {
                if (connection == null)
                {
                    throw new InvalidOperationException("Unable to create a SQL connection.");
                }

                await connection.OpenAsync();

                // Retrieve the user ID based on the username
                var userId = await connection.QuerySingleOrDefaultAsync<int>(
                    "SELECT Id FROM Users WHERE Username = @Username",
                    new { Username = username }
                );

                if (userId == 0)
                {
                    return false; // User not found
                }

                // Retrieve the role ID based on the role name
                var roleId = await connection.QuerySingleOrDefaultAsync<int>(
                    "SELECT Id FROM Roles WHERE RoleName = @RoleName",
                    new {RoleName= roleName }
                );

                if (roleId == 0)
                {
                    return false; // Role not found
                }

                // Remove the role from the user
                var rowsAffected = await connection.ExecuteAsync(
                    "DELETE FROM UserRoles WHERE UserId = @UserId AND RoleId = @RoleId",
                    new { UserId = userId, RoleId = roleId }
                );

                return rowsAffected > 0; // Return true if a row was deleted
            }
        }














    }
}

