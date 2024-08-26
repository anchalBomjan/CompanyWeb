using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;
using WebApp.API.Models.DTOs;
using WebApp.API.Repositories.IRepository;
using WebApp.API.Services.IServices;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAssigningDetailsController : ControllerBase
    {
        private readonly IEmployeeDetailRepository _employeeDetailRepository;
        public EmployeeAssigningDetailsController(IEmployeeDetailRepository employeeDetailRepository)
        {
             _employeeDetailRepository = employeeDetailRepository;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateEmployeeDetail(EmployeeDetailCreateDTO createDto)
        {
            var createdEmployeeDetail = await _employeeDetailRepository.CreateEmployeeDetailAsync(createDto);
            if (createdEmployeeDetail == null)
            {
                return BadRequest("Failed to create EmployeeDetail.");
            }
            return CreatedAtAction(nameof(GetEmployeeDetailById), new { id = createdEmployeeDetail.EmployeeDetailId }, createdEmployeeDetail);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeDetails()
        {
            var employeeDetails = await _employeeDetailRepository.GetAllEmployeeDetailsAsync();
            return Ok(employeeDetails);
        }
        // Read (Get by ID)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeDetailById(int id)
        {
            var employeeDetail = await _employeeDetailRepository.GetEmployeeDetailByIdAsync(id);
            if (employeeDetail == null)
            {
                return NotFound();
            }
            return Ok(employeeDetail);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeDetail(int id, EmployeeDetailUpdateDTO updateDto)
        {
            if (id != updateDto.EmployeeDetailId)
            {
                return BadRequest("EmployeeDetail ID mismatch.");
            }

            var updated = await _employeeDetailRepository.UpdateEmployeeDetailAsync(updateDto);
            if (!updated)
            {
                return BadRequest("Failed to update EmployeeDetail.");
            }
            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeDetail(int id)
        {
            var deleted = await _employeeDetailRepository.DeleteEmployeeDetailAsync(id);
            if (!deleted)
            {
                return BadRequest("Failed to delete EmployeeDetail.");
            }
            return NoContent();
        }

    }
}
