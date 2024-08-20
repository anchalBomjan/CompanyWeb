using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;
using WebApp.API.Models.DTOs;
using WebApp.API.Repositories.IRepository;
using WebApp.API.Services;
using WebApp.API.Services.IServices;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPhotoService _photoService;

        public EmployeeController(IEmployeeRepository employeeRepository, IPhotoService photoService)
        {
            _employeeRepository = employeeRepository;
            _photoService = photoService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromForm] EmployeeDto employeeDto, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string imageUrl = null;

            // Check if a file is uploaded
            if (file != null && file.Length > 0)
            {
                var uploadResult = await _photoService.AddPhotoAsync(file);
                if (uploadResult.Error != null)
                {
                    return BadRequest(uploadResult.Error.Message);
                }

                // Set the imageUrl to the uploaded file's URL
                imageUrl = uploadResult.SecureUrl.ToString();
            }

            // Create the employee object and set ImageUrl if available
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Phone = employeeDto.Phone,
                ImageUrl = imageUrl,  // This is set after the file is uploaded
                DateOfBirth = employeeDto.DateOfBirth,
                Address = employeeDto.Address,
                HireDate = employeeDto.HireDate,
            };

            _employeeRepository.Add(employee);
            await _employeeRepository.SaveAllAsync();

            return Ok(employee);
        }

        // Additional CRUD operations (GET, PUT, DELETE) can be implemented similarly
    

    // READ Operation
    [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // UPDATE Operation
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromForm] EmployeeDto employeeDto, IFormFile file)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            if (file != null)
            {
                var uploadResult = await _photoService.AddPhotoAsync(file);
                if (uploadResult.Error != null)
                    return BadRequest(uploadResult.Error.Message);

                if (!string.IsNullOrEmpty(employee.ImageUrl))
                {
                    var publicId = employee.ImageUrl.Split('/').Last().Split('.').First();
                    await _photoService.DeletePhotoAsync(publicId);
                }

                employee.ImageUrl = uploadResult.SecureUrl.ToString();
            }

            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.DateOfBirth = employeeDto.DateOfBirth;
            employee.Address = employeeDto.Address;
            employee.HireDate = employeeDto.HireDate;

            _employeeRepository.Update(employee);
            await _employeeRepository.SaveAllAsync();

            return Ok(employee);
        }

        // DELETE Operation
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            if (!string.IsNullOrEmpty(employee.ImageUrl))
            {
                var publicId = employee.ImageUrl.Split('/').Last().Split('.').First();
                await _photoService.DeletePhotoAsync(publicId);
            }

            _employeeRepository.Delete(employee);
            await _employeeRepository.SaveAllAsync();

            return Ok();
        }


    }

    
}
