using WebApp.API.Models;
using WebApp.API.Models.DTOs;

namespace WebApp.API.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<int> RegisterUserAsync(User user, string passwordHash);


        // return type User to store the Date of Users Table in User model and aslo synchronized its telated roles of the Users 
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string username, string email);

        Task<List<UserWithRolesDTO>> GetAllUsersWithRolesAsync();

        


    }
}
