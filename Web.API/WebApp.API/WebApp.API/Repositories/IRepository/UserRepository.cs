using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApp.API.Data;
using WebApp.API.Models;

namespace WebApp.API.Repositories.IRepository
{
    public class UserRepository:IUserRepository
    {


        private readonly DbConnector _dbConnector;

        public UserRepository(DbConnector dbConnector)
        {
            _dbConnector = dbConnector ?? throw new ArgumentNullException(nameof(dbConnector));
        }

        public async Task<int> RegisterUserAsync(User user, string passwordHash)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(passwordHash)) throw new ArgumentException("PasswordHash cannot be null or empty.", nameof(passwordHash));

            const string sql = @"
                INSERT INTO Users (Username, Email, PasswordHash, FirstName, LastName)
                VALUES (@Username, @Email, @PasswordHash, @FirstName, @LastName);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var connection = _dbConnector.CreateConnection() as SqlConnection)
            {
                if (connection == null)
                    throw new InvalidOperationException("Failed to create a SQL connection.");

                await connection.OpenAsync(); // Explicitly open the connection
                return await connection.QuerySingleAsync<int>(sql, new
                {
                    user.Username,
                    user.Email,
                    PasswordHash = passwordHash,
                    user.FirstName,
                    user.LastName
                });
            }

        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));

            const string sql = "SELECT * FROM Users WHERE Username = @Username";

            using (var connection = _dbConnector.CreateConnection() as SqlConnection)
            {
                if (connection == null)
                    throw new InvalidOperationException("Failed to create a SQL connection.");

                await connection.OpenAsync(); // Explicitly open the connection
                return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
            }

        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));

            const string sql = "SELECT * FROM Users WHERE Email = @Email";

            using (var connection = _dbConnector.CreateConnection() as SqlConnection)
            {
                if (connection == null)
                    throw new InvalidOperationException("Failed to create a SQL connection.");

                await connection.OpenAsync(); // Explicitly open the connection
                return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
            }

        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(email))
                throw new ArgumentException("At least one of username or email must be provided.");

            const string sql = @"
                SELECT COUNT(1) 
                FROM Users 
                WHERE Username = @Username OR Email = @Email";

            using (var connection = _dbConnector.CreateConnection() as SqlConnection)
            {
                if (connection == null)
                    throw new InvalidOperationException("Failed to create a SQL connection.");

                await connection.OpenAsync(); // Explicitly open the connection
                var count = await connection.ExecuteScalarAsync<int>(sql, new { Username = username, Email = email });
                return count > 0;
            }
        }

    }
}
