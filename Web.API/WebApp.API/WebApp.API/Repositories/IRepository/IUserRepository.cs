using WebApp.API.Models;

namespace WebApp.API.Repositories.IRepository
{
    public interface IUserRepository
    {
        Task<int> RegisterUserAsync(User user, string passwordHash);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string username, string email);
    }
}
