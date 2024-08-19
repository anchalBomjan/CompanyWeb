using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;
using WebApp.API.Models.DTOs;
using WebApp.API.Repositories.IRepository;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
             _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            //var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            //return Ok(departmentDtos);

            return Ok(departments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult>GetDepartment(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound();

            //var departmentDto = _mapper.Map<DepartmentDto>(department);
            //return Ok(departmentDto);


            return Ok(department);

           
        }
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = _mapper.Map<Department>(departmentDto);
            var createdDepartment = await _departmentRepository.AddDepartmentAsync(department);
            var createdDepartmentDto = _mapper.Map<DepartmentDto>(createdDepartment);

            return Ok(createdDepartmentDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound();

            _mapper.Map(departmentDto, department);
            await _departmentRepository.UpdateDepartmentAsync(department);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            if (department == null)
                return NotFound();

            await _departmentRepository.DeleteDepartmentAsync(id);
            return NoContent();
        }


        [HttpGet("GetDepartmentsWithDesignations")]
        public async Task<IActionResult> GetDepartmentsWithDesignations()
        {
            var departments = await _departmentRepository.GetDepartmentsWithDesignationsAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentWithDesignationsDto>>(departments);
            return Ok(departmentDtos);
        }
    }
}
