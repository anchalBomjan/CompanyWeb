using WebApp.API.Models;

namespace WebApp.API.Repositories.IRepository
{
    public interface IDesignationRepository
    {

        Task<IEnumerable<Designation>> GetAllDesignationsAsync();
        Task<Designation> GetDesignationByIdAsync(int id);
        Task<Designation> AddDesignationAsync(Designation designation);
        Task<Designation> UpdateDesignationAsync(Designation designation);
        Task DeleteDesignationAsync(int id);
    }
}
