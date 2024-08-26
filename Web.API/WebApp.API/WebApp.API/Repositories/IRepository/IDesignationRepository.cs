using WebApp.API.Models;
using WebApp.API.Models.DTOs;

namespace WebApp.API.Repositories.IRepository
{
    public interface IDesignationRepository
    {

        Task<IEnumerable<Designation>> GetAllDesignationsAsync();
        Task<Designation> GetDesignationByIdAsync(int id);
        Task<Designation> AddDesignationAsync(Designation designation);
        Task<Designation> UpdateDesignationAsync(Designation designation);
        Task DeleteDesignationAsync(int id);



        Task<IEnumerable<Designation>> GetDesignationsByDepartmentAsync(int departmentId);
    }
}
