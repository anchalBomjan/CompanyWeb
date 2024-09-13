using BCrypt.Net;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using WebApp.API.Data;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;
using WebApp.API.Services;

public class AuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;
    private readonly DbConnector _dbConnector;

    public AuthenticationService(IUserRepository userRepository, JwtTokenService jwtTokenService, DbConnector dbConnector)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
        _dbConnector = dbConnector;
    }

    public async Task<string> RegisterAsync(User user, string password)
    {

        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password cannot be null or empty.");

        var exists = await _userRepository.UserExistsAsync(user.Username, user.Email);
        if (exists)
        {
            throw new Exception("User already exists.");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var userId = await _userRepository.RegisterUserAsync(user, passwordHash);
        user.Id = userId;

        // Assign the default role to the user
        using (var connection = _dbConnector.CreateConnection() as SqlConnection)
        {
            if (connection == null)
                throw new InvalidOperationException("Failed to create a SQL connection.");

            await connection.OpenAsync(); // Explicitly open the connection

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Retrieve the "User" role ID from the Roles table
                    var roleId = await connection.QuerySingleOrDefaultAsync<int>(
                        "SELECT Id FROM Roles WHERE RoleName = @RoleName",
                        new { RoleName = StaticUserRoles.USER },
                        transaction: transaction
                    );

                    if (roleId == 0)
                    {
                        throw new InvalidOperationException("Default role not found.");
                    }

                    // Assign the "User" role to the new user
                    var rowsAffected = await connection.ExecuteAsync(
                        "INSERT INTO UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)",
                        new { UserId = user.Id, RoleId = roleId },
                        transaction: transaction
                    );

                    if (rowsAffected == 0)
                    {
                        throw new ApplicationException("Failed to assign the default role to the user.");
                    }

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    transaction.Rollback();
                    // Log the exception details or handle it as needed
                    throw new ApplicationException("An error occurred while assigning the default role to the user.", ex);
                }
            }
        }




        return _jwtTokenService.GenerateToken(user);

    }

    public async Task<string> LoginAsync(string username, string password)
    {


        if (string.IsNullOrEmpty(username)) throw new ArgumentException("Username cannot be null or empty.");
        if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password cannot be null or empty.");

        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null || string.IsNullOrEmpty(user.PasswordHash) || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            throw new Exception("Invalid credentials.");
        }

        return _jwtTokenService.GenerateToken(user);
    }
}
