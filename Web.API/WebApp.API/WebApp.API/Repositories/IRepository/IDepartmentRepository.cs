using WebApp.API.Models;

namespace WebApp.API.Repositories.IRepository
{
    public interface IDepartmentRepository
    {

        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department>AddDepartmentAsync(Department department);
        Task<Department> UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
        Task<IEnumerable<Department>> GetDepartmentsWithDesignationsAsync();

    }
}
