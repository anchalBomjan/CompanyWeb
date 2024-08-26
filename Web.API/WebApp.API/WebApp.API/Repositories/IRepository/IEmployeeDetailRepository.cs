using WebApp.API.Models.DTOs;

namespace WebApp.API.Repositories.IRepository
{
    public interface IEmployeeDetailRepository
    {
        Task<EmployeeDetailDTO> CreateEmployeeDetailAsync(EmployeeDetailCreateDTO createDto);
        Task<List<EmployeeDetailDTO>> GetAllEmployeeDetailsAsync();
        Task<EmployeeDetailDTO> GetEmployeeDetailByIdAsync(int id);
        Task<bool> UpdateEmployeeDetailAsync(EmployeeDetailUpdateDTO updateDto);
        Task<bool> DeleteEmployeeDetailAsync(int id);

    }
}
