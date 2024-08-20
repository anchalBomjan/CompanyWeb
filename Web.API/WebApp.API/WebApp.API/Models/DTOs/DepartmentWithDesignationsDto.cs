namespace WebApp.API.Models.DTOs
{
    public class DepartmentWithDesignationsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DesignationDto> Designations { get; set; }

    }
}
