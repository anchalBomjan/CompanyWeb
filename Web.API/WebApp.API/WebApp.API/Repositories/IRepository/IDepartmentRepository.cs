
using WebApp.API.Models.DTOs;

namespace WebApp.API.Repositories.IRepository
{
    public interface IDepartmentRepository
    {


        Task<DepartmentDto> AddAsync(DepartmentDto department);
        Task<DepartmentDto> GetByIdAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task UpdateAsync(DepartmentDto department);
        Task DeleteAsync(int id);
    }
}
