using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using AutoMapper;

namespace WebApp.API
{
    public class MappingProfile:Profile
    {
       public MappingProfile() {

            CreateMap<Department, DepartmentWithDesignationsDto>()
              .ForMember(dest => dest.Designations, opt => opt.MapFrom(src => src.Designations));
            // Map Employee to EmployeeDto
           

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Designation, DesignationDto>().ReverseMap();





            CreateMap<EmployeeDetail, EmployeeDetailDTO>()
           .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
           .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
           .ForMember(dest => dest.DesignationTitle, opt => opt.MapFrom(src => src.Designation.Title));

            CreateMap<EmployeeDetailCreateDTO, EmployeeDetail>();
            CreateMap<EmployeeDetailUpdateDTO, EmployeeDetail>();
        }
    }
}
