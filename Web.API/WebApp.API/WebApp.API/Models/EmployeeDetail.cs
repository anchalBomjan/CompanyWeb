namespace WebApp.API.Models
{
    public class EmployeeDetail
    {
        public int EmployeeDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int? DesignationId { get; set; }

        public Employee Employee { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
    }
}
