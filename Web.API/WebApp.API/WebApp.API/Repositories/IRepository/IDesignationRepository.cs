
using WebApp.API.Models.DTOs;

namespace WebApp.API.Repositories.IRepository
{
    public interface IDesignationRepository
    {
        Task<DesignationDto> AddAsync(DesignationDto designation);
        Task<DesignationDto> GetByIdAsync(int id);
        Task<IEnumerable<DesignationDto>> GetAllAsync();
        Task UpdateAsync(DesignationDto designation);
        Task DeleteAsync(int id);

    }
}
