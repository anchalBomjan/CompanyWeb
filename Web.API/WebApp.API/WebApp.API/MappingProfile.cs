using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using AutoMapper;

namespace WebApp.API
{
    public class MappingProfile:Profile
    {
       public MappingProfile() {
            // Mapping from Department to DepartmentWithDesignationsDto
            CreateMap<Department, DepartmentWithDesignationsDto>()
                .ForMember(dest => dest.Designations, opt => opt.MapFrom(src => src.Designations));

            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Designation, DesignationDto>().ReverseMap();
        }
    }
}
