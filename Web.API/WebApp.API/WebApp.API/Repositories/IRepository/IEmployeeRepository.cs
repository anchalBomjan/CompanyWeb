using WebApp.API.Models;
using WebApp.API.Models.DTOs;

namespace WebApp.API.Repositories.IRepository
{
    public interface IEmployeeRepository
    {

        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);

    }
}
