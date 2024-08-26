namespace WebApp.API.Models.DTOs
{
    public class EmployeeDetailDTO
    {

        public int EmployeeDetailId { get; set; } // Populated after creation
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationTitle { get; set; }
    }
}
