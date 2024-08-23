using WebApp.API.Models;

namespace WebApp.API.Repositories.IRepository
{
    public interface IEmployeeDetailRepository
    {

        Task<IEnumerable<EmployeeDetail>> GetAllAsync();
        Task<EmployeeDetail> GetByIdAsync(int id);
        Task AddAsync(EmployeeDetail employeeDetail);
        Task UpdateAsync(EmployeeDetail employeeDetail);
        Task DeleteAsync(int id);
    }
}
