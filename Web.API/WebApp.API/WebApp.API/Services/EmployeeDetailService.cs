using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;
using WebApp.API.Services.IServices;

namespace WebApp.API.Services
{
    public class EmployeeDetailService : IEmployeeDetailService
    {

        private readonly IEmployeeDetailRepository _repository;

        public EmployeeDetailService(IEmployeeDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeDetail>> GetAllEmployeeDetailsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<EmployeeDetail> GetEmployeeDetailByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddEmployeeDetailAsync(EmployeeDetail employeeDetail)
        {
            await _repository.AddAsync(employeeDetail);
        }

        public async Task UpdateEmployeeDetailAsync(EmployeeDetail employeeDetail)
        {
            await _repository.UpdateAsync(employeeDetail);
        }

        public async Task DeleteEmployeeDetailAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
