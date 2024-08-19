namespace WebApp.API.Models.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IFormFile Image { get; set; } // For handling image upload
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateTime HireDate { get; set; }
    }
}
