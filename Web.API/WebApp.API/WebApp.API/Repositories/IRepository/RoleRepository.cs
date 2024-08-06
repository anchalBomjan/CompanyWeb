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

    }
}

