using Microsoft.EntityFrameworkCore;
using WebApp.API.Data;
using WebApp.API.Models;
using WebApp.API.Models.DTOs;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Repositories
{
    public class DesignationRepository :IDesignationRepository
    {

        private readonly ApplicationDbContext _context;
        public DesignationRepository(ApplicationDbContext context)
        {
             _context= context;
        }
        public async Task<IEnumerable<Designation>> GetAllDesignationsAsync()
        {
            return await _context.Designations.ToListAsync();
        }

        public async Task<Designation> GetDesignationByIdAsync(int id)
        {
            return await _context.Designations.FindAsync(id);
        }

        public async Task<Designation> AddDesignationAsync(Designation designation)
        {
            _context.Designations.Add(designation);
            await _context.SaveChangesAsync();
            return designation;
        }

        public async Task<Designation> UpdateDesignationAsync(Designation designation)
        {
            _context.Designations.Update(designation);
            await _context.SaveChangesAsync();
            return designation;
        }

        public async Task DeleteDesignationAsync(int id)
        {
            var designation = await _context.Designations.FindAsync(id);
            if (designation != null)
            {
                _context.Designations.Remove(designation);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<IEnumerable<Designation>> GetDesignationsByDepartmentAsync(int departmentId)
        {
            return await _context.Designations
                .Where(d => d.DepartmentId == departmentId)
                .ToListAsync();
        }
    }
}
