namespace WebApp.API.Repositories.IRepository
{
    public interface IRoleRepository
    {

        //Task  SeedRolesAsync();

        Task<List<string>> SeedRolesAsync();
        Task<bool> AssignRoleToUserAsync(string username, string rolename);

        Task<List<string>> GetAllRolesAsync();


        Task<bool> RemoveRoleFromUserRolesAsync(string username, string rolename);

       

    }
}
