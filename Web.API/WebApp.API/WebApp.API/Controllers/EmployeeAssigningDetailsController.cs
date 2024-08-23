using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;
using WebApp.API.Services.IServices;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAssigningDetailsController : ControllerBase
    {
        private readonly IEmployeeDetailService _service;

        public EmployeeAssigningDetailsController(IEmployeeDetailService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeDetails()
        {
            var employeeDetails = await _service.GetAllEmployeeDetailsAsync();
            return Ok(employeeDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeDetailById(int id)
        {
            var employeeDetail = await _service.GetEmployeeDetailByIdAsync(id);
            if (employeeDetail == null)
            {
                return NotFound();
            }
            return Ok(employeeDetail);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeDetail([FromBody] EmployeeDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                await _service.AddEmployeeDetailAsync(employeeDetail);
                return CreatedAtAction(nameof(GetEmployeeDetailById), new { id = employeeDetail.EmployeeDetailId }, employeeDetail);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeDetail(int id, [FromBody] EmployeeDetail employeeDetail)
        {
            if (id != employeeDetail.EmployeeDetailId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _service.UpdateEmployeeDetailAsync(employeeDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeDetail(int id)
        {
            await _service.DeleteEmployeeDetailAsync(id);
            return NoContent();
        }

    }
}
