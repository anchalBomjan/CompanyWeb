namespace WebApp.API.Models.DTOs
{
    public class UserWithRolesDTO
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();

    }
}
