using Microsoft.EntityFrameworkCore;
using WebApp.API.Data;
using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Repositories
{
    public class EmployeeDetailRepository: IEmployeeDetailRepository
    {

        private readonly ApplicationDbContext _context;
        public EmployeeDetailRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<EmployeeDetailDTO> CreateEmployeeDetailAsync(EmployeeDetailCreateDTO createDto)
        {
            var employeeDetail = new EmployeeDetail
            {
                EmployeeId = createDto.EmployeeId,
                DepartmentId = createDto.DepartmentId,
                DesignationId = createDto.DesignationId
            };

            _context.EmployeeDetails.Add(employeeDetail);
            await _context.SaveChangesAsync();

            return new EmployeeDetailDTO
            {
                EmployeeDetailId = employeeDetail.EmployeeDetailId,
                EmployeeName = _context.Employees.Find(employeeDetail.EmployeeId)?.Name,
                DepartmentName = _context.Departments.Find(employeeDetail.DepartmentId)?.Name,
                DesignationTitle = _context.Designations.Find(employeeDetail.DesignationId)?.Title
            };
        }


        public async Task<List<EmployeeDetailDTO>> GetAllEmployeeDetailsAsync()
        {
            var employeeDetails = await _context.EmployeeDetails
                .Include(ed => ed.Employee)
                .Include(ed => ed.Department)
                .Include(ed => ed.Designation)
                .ToListAsync();

            return employeeDetails.Select(ed => new EmployeeDetailDTO
            {
                EmployeeDetailId = ed.EmployeeDetailId,
                EmployeeName = ed.Employee?.Name,
                DepartmentName = ed.Department?.Name,
                DesignationTitle = ed.Designation?.Title
            }).ToList();
        }
        public async Task<EmployeeDetailDTO> GetEmployeeDetailByIdAsync(int id)
        {
            var employeeDetail = await _context.EmployeeDetails
                .Include(ed => ed.Employee)
                .Include(ed => ed.Department)
                .Include(ed => ed.Designation)
                .FirstOrDefaultAsync(ed => ed.EmployeeDetailId == id);

            if (employeeDetail == null) return null;

            return new EmployeeDetailDTO
            {
                EmployeeDetailId = employeeDetail.EmployeeDetailId,
                EmployeeName = employeeDetail.Employee?.Name,
                DepartmentName = employeeDetail.Department?.Name,
                DesignationTitle = employeeDetail.Designation?.Title
            };
        }

        public async Task<bool> UpdateEmployeeDetailAsync(EmployeeDetailUpdateDTO updateDto)
        {
            var employeeDetail = await _context.EmployeeDetails.FindAsync(updateDto.EmployeeDetailId);
            if (employeeDetail == null) return false;

            employeeDetail.EmployeeId = updateDto.EmployeeId;
            employeeDetail.DepartmentId = updateDto.DepartmentId;
            employeeDetail.DesignationId = updateDto.DesignationId;

            _context.EmployeeDetails.Update(employeeDetail);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEmployeeDetailAsync(int id)
        {
            var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetail == null) return false;

            _context.EmployeeDetails.Remove(employeeDetail);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
