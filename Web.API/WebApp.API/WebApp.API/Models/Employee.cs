using System.ComponentModel.DataAnnotations;

namespace WebApp.API.Models
{
    public class Employee
    {

        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateTime HireDate { get; set; }

        public string? PublicId { get; set; } 

        public ICollection<EmployeeDetail> EmployeeDetails { get; set; }
    }
}
