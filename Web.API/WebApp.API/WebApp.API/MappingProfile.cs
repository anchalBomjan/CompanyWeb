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




         
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
                .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.RecipientId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.DateRead, opt => opt.MapFrom(src => src.DateRead))
                .ForMember(dest => dest.MessageSent, opt => opt.MapFrom(src => src.MessageSent))
                .ForMember(dest => dest.SenderUsername, opt => opt.MapFrom(src => src.SenderUsername)) // Add this if required
                .ForMember(dest => dest.RecipientUsername, opt => opt.MapFrom(src => src.RecipientUsername)); // Add this if required





        }
    }
}
