using Microsoft.EntityFrameworkCore;
using WebApp.API.Data;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Repositories
{
    public class EmployeeDetailRepository:IEmployeeDetailRepository
    {

        private readonly ApplicationDbContext _context;
        public EmployeeDetailRepository(ApplicationDbContext context)
        {

            _context = context;

        }

        public async Task<IEnumerable<EmployeeDetail>> GetAllAsync()
        {
            return await _context.EmployeeDetails
                                 .Include(ed => ed.Employee)
                                 .Include(ed => ed.Department)
                                 .Include(ed => ed.Designation)
                                 .ToListAsync();
        }

        public async Task<EmployeeDetail> GetByIdAsync(int id)
        {
            return await _context.EmployeeDetails
                                 .Include(ed => ed.Employee)
                                 .Include(ed => ed.Department)
                                 .Include(ed => ed.Designation)
                                 .FirstOrDefaultAsync(ed => ed.EmployeeDetailId == id);
        }

        public async Task AddAsync(EmployeeDetail employeeDetail)
        {
            _context.EmployeeDetails.Add(employeeDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EmployeeDetail employeeDetail)
        {
            _context.EmployeeDetails.Update(employeeDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetail != null)
            {
                _context.EmployeeDetails.Remove(employeeDetail);
                await _context.SaveChangesAsync();
            }


        }
    }
}
