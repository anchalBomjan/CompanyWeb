using WebApp.API.Models;

namespace WebApp.API.Services.IServices
{
    public interface IEmployeeDetailService
    {

        Task<IEnumerable<EmployeeDetail>> GetAllEmployeeDetailsAsync();
        Task<EmployeeDetail> GetEmployeeDetailByIdAsync(int id);
        Task AddEmployeeDetailAsync(EmployeeDetail employeeDetail);
        Task UpdateEmployeeDetailAsync(EmployeeDetail employeeDetail);
        Task DeleteEmployeeDetailAsync(int id);
    }
}
