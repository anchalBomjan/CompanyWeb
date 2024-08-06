namespace WebApp.API.Models
{
    public class Employee
    {

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateTime HireDate { get; set; }

        public ICollection<EmployeeDetail> EmployeeDetails { get; set; }
    }
}
