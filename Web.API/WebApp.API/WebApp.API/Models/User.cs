namespace WebApp.API.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation property for the roles associated with the user
        public List<Role> Roles { get; set; } = new List<Role>();

   
    }
}
