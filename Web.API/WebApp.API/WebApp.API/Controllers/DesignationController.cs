using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;
using WebApp.API.Models.DTOs;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {

        private readonly IDesignationRepository _designationRepository;

        public DesignationController(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var designations = await _designationRepository.GetAllAsync();
            return Ok(designations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var designation = await _designationRepository.GetByIdAsync(id);
            if (designation == null)
                return NotFound();
            return Ok(designation);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DesignationDto designationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdDesignation = await _designationRepository.AddAsync(designationDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DesignationDto designationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var designation = await _designationRepository.GetByIdAsync(id);
            if (designation == null)
                return NotFound();

            await _designationRepository.UpdateAsync(designationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var designation = await _designationRepository.GetByIdAsync(id);
            if (designation == null)
                return NotFound();

            await _designationRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
