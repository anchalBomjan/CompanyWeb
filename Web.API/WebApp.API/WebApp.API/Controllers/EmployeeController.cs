
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;
using WebApp.API.Services.IServices;



namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
       private readonly IEmployeeRepository _employeeRepository;
       private readonly IPhotoServices _photoServices;

        public EmployeeController(IEmployeeRepository employeeRepository,IPhotoServices photoServices)
        {
             _employeeRepository = employeeRepository;
            _photoServices = photoServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            if (employees == null || !employees.Any())
                return NotFound("No employees found");

            return Ok(employees);
        }
        // GET api/employees/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound(); 
            }

            return Ok(employee); 
        }



        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeDto employeeDto)
        {
            if (employeeDto.Image == null || employeeDto.Image.Length == 0)
                return BadRequest("Image is required");

            var uploadResult = await _photoServices.AddPhotoAsync(employeeDto.Image);

            if (uploadResult.Error != null)
                return BadRequest(uploadResult.Error.Message);

            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                ImageUrl = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId, // Save the public ID
                DateOfBirth = employeeDto.DateOfBirth,
                Address = employeeDto.Address,
                HireDate = employeeDto.HireDate
            };

            await _employeeRepository.AddEmployeeAsync(employee);

            return Ok(employee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromForm] EmployeeDto employeeDto)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            // Update employee properties
            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.DateOfBirth = employeeDto.DateOfBirth;
            employee.Address = employeeDto.Address;
            employee.HireDate = employeeDto.HireDate;

            // Handle image if present
            if (employeeDto.Image != null && employeeDto.Image.Length > 0)
            {
                var uploadResult = await _photoServices.AddPhotoAsync(employeeDto.Image);

                if (uploadResult.Error != null)
                    return BadRequest(uploadResult.Error.Message);

                if (!string.IsNullOrEmpty(employee.PublicId))
                {
                    var deletionResult = await _photoServices.DeletePhotoAsync(employee.PublicId);

                    if (deletionResult.Error != null)
                        return BadRequest(deletionResult.Error.Message);
                }

                employee.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;
                employee.PublicId = uploadResult.PublicId; // Update the public ID
            }
         
            await _employeeRepository.UpdateEmployeeAsync(employee);

            return Ok(employee);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            if (!string.IsNullOrEmpty(employee.PublicId))
            {
                var deletionResult = await _photoServices.DeletePhotoAsync(employee.PublicId);

                if (deletionResult.Error != null)
                    return BadRequest(deletionResult.Error.Message);

                if (deletionResult.Result == "not found")
                    return BadRequest("Photo not found.");
            }

            await _employeeRepository.DeleteEmployeeAsync(id);

            return Ok();
        }



    }
}

  

  
