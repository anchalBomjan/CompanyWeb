using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApp.API.Data
{
    public class DbConnector
    {
        private readonly IConfiguration _configuration;


        public DbConnector(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // Method to create and return a SQL Server connection
        public IDbConnection CreateConnection()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            return new SqlConnection(connectionString); // Use SqlConnection for SQL Server
        }
    }
}
