namespace WebApp.API.Repositories.IRepository
{
    public interface IRoleRepository
    {

        Task  SeedRolesAsync();
        Task<bool> AssignRoleToUserAsync(string username, string rolename);

    }
}
