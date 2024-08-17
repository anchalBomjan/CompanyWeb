using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models.DTOs;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IMapper _mapper;

        public DesignationController(IDesignationRepository designationRepository, IMapper mapper)
        {
             _designationRepository = designationRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetDesignations()
        {
            var designations = await _designationRepository.GetAllDesignationsAsync();
            // var designationDtos = _mapper.Map<IEnumerable<DesignationDto>>(designations);
            //return Ok(designationDtos);

            return Ok(designations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesignation(int id)
        {
            var designation = await _designationRepository.GetDesignationByIdAsync(id);
            if (designation == null)
                return NotFound();

            var designationDto = _mapper.Map<DesignationDto>(designation);
            return Ok(designationDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDesignation([FromBody] DesignationDto designationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var designation = _mapper.Map<Designation>(designationDto);
            var createdDesignation = await _designationRepository.AddDesignationAsync(designation);
            var createdDesignationDto = _mapper.Map<DesignationDto>(createdDesignation);

            return CreatedAtAction(nameof(GetDesignation), new { id = createdDesignation.DesignationId }, createdDesignationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDesignation(int id, [FromBody] DesignationDto designationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var designation = await _designationRepository.GetDesignationByIdAsync(id);
            if (designation == null)
                return NotFound();

            _mapper.Map(designationDto, designation);
            await _designationRepository.UpdateDesignationAsync(designation);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            var designation = await _designationRepository.GetDesignationByIdAsync(id);
            if (designation == null)
                return NotFound();

            await _designationRepository.DeleteDesignationAsync(id);
            return NoContent();
        }
    }
}




