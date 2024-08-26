namespace WebApp.API.Models.DTOs
{
    public class EmployeeDetailCreateDTO
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int? DesignationId { get; set; }
    }
}
