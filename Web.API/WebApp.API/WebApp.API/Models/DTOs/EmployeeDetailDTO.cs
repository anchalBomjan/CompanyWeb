namespace WebApp.API.Models.DTOs
{
    public class EmployeeDetailDTO
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; } // Nullable if it's optional

    }
}
