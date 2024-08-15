using Microsoft.EntityFrameworkCore;
using WebApp.API.Data;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Repositories
{
    public class DepartmentRepository :IDepartmentRepository
    {

        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
             _context= context;
        }
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }


        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
