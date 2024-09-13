using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.API.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }



        [ForeignKey("UserId")]

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
