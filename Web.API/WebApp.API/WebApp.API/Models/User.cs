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






        //  below  code for   is use during   Messages  work   to navigate     beacause   Messages  work is done from   Entityframework
        // Navigation property for the roles associated with the user

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }



}
