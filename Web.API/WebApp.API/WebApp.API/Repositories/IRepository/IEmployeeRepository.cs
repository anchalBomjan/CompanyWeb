using WebApp.API.Models;

namespace WebApp.API.Repositories.IRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        Task<bool> SaveAllAsync();
    }
}
