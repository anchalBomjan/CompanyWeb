using System.Text.Json.Serialization;

namespace WebApp.API.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<Designation> Designations { get; set; }
      
        public ICollection<EmployeeDetail> EmployeeDetails { get; set; }
       



    }
}
