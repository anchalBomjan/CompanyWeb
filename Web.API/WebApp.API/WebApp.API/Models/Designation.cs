using System.Text.Json.Serialization;

namespace WebApp.API.Models
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }
        public string Description { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
      
        public ICollection<EmployeeDetail> EmployeeDetails { get; set; }
    }
}
