using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using AutoMapper;

namespace WebApp.API
{
    public class MappingProfile:Profile
    {
       public MappingProfile() {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<Designation, DesignationDto>().ReverseMap();
        }
    }
}
