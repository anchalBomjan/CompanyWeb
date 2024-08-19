using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                DateOfBirth = employeeDto.DateOfBirth,
                Address = employeeDto.Address,
                HireDate = employeeDto.HireDate,
            };

            if (employeeDto.Image != null)
            {
                using var ms = new MemoryStream();
                await employeeDto.Image.CopyToAsync(ms);
                employee.ImageData = ms.ToArray();
            }

            var createdEmployee = await _employeeRepository.AddEmployeeAsync(employee);

            return Ok(createdEmployee);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        // READ
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromForm] EmployeeDto employeeDto)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = employeeDto.Name;
            existingEmployee.Email = employeeDto.Email;
            existingEmployee.Phone = employeeDto.Phone;
            existingEmployee.DateOfBirth = employeeDto.DateOfBirth;
            existingEmployee.Address = employeeDto.Address;
            existingEmployee.HireDate = employeeDto.HireDate;

            if (employeeDto.Image != null)
            {
                using var ms = new MemoryStream();
                await employeeDto.Image.CopyToAsync(ms);
                existingEmployee.ImageData = ms.ToArray();
            }

            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(existingEmployee);

            return Ok(updatedEmployee);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeRepository.DeleteEmployeeAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok();

        }
    }
}
