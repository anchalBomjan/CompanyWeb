using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;
using WebApp.API.Services.IServices;
using WebApp.API.Services;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet.Actions;
using WebApp.API.Helper;


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

            if (employeeDto.Image != null && employeeDto.Image.Length > 0)
            {
                // Upload new photo and handle result
                var uploadResult = await _photoServices.AddPhotoAsync(employeeDto.Image);

                if (uploadResult.Error != null)
                    return BadRequest(uploadResult.Error.Message);

                // Optionally delete the old image if it exists
                if (!string.IsNullOrEmpty(employee.ImageUrl))
                {
                    var publicId = CloudinaryHelper.ExtractPublicId(employee.ImageUrl);
                    var deletionResult = await _photoServices.DeletePhotoAsync(publicId);

                    if (deletionResult.Error != null)
                        return BadRequest(deletionResult.Error.Message);
                }

                // Set the new image URL
                employee.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;
            }

            // Update other employee properties
            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.Address = employeeDto.Address;
            employee.DateOfBirth = employeeDto.DateOfBirth;
            employee.HireDate = employeeDto.HireDate;

            // Save updated employee to the repository
            await _employeeRepository.UpdateEmployeeAsync(employee);

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            if (!string.IsNullOrEmpty(employee.ImageUrl))
            {
                // Extract public ID from the image URL
                var publicId = CloudinaryHelper.ExtractPublicId(employee.ImageUrl);

                // Attempt to delete the photo from Cloudinary
                var deletionResult = await _photoServices.DeletePhotoAsync(publicId);

                // Check if there was an error in the deletion result
                if (deletionResult.Error != null)
                    return BadRequest(deletionResult.Error.Message);

                // Alternatively, you might want to check if the result indicates a failure
                if (deletionResult.Result == "not found")
                    return BadRequest("Photo not found.");
            }

            // Delete the employee record from the database
            await _employeeRepository.DeleteEmployeeAsync(id);

            return Ok();
        }



    }
}

  

  
