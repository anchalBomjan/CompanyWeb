namespace WebApp.API.Models.DTOs
{
    public class EmployeeDetailUpdateDTO
    {
        public int EmployeeDetailId { get; set; } // Required for updates
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int? DesignationId { get; set; }
    }
}
