using Microsoft.EntityFrameworkCore;
using WebApp.API.Data;

using WebApp.API.Models.DTOs;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Repositories
{
    public class DesignationRepository:IDesignationRepository
    {
        private readonly ApplicationDbContext _context;

        public DesignationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DesignationDto> AddAsync(DesignationDto designation)
        {
            await _context.Designations.AddAsync(designation);
            await _context.SaveChangesAsync();
            return designation;
        }

        public async Task<DesignationDto> GetByIdAsync(int id)
        {
            return await _context.Designations.FindAsync(id);
        }

        public async Task<IEnumerable<DesignationDto>> GetAllAsync()
        {
            return await _context.Designations.ToListAsync();
        }

        public async Task UpdateAsync(DesignationDto designation)
        {
            _context.Designations.Update(designation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var designation = await _context.Designations.FindAsync(id);
            if (designation != null)
            {
                _context.Designations.Remove(designation);
                await _context.SaveChangesAsync();
            }
        }

    }
}
