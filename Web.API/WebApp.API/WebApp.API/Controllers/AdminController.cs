using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;
using WebApp.API.Repositories.IRepository;
using WebApp.API.Services;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {


        private readonly  IRoleRepository _roleRepository;

        private readonly IUserRepository _userRepository;

        public AdminController(IRoleRepository rolerepository,IUserRepository userRepository)
        {
            _roleRepository = rolerepository;
            _userRepository = userRepository;
             
        }



        [HttpPost("SeedRoles")]

        public async Task<IActionResult> SeedRoles()
        {
            try
            {
                var messages = await _roleRepository.SeedRolesAsync();
                return Ok(new { Messages = messages });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }






        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRoleToUser([FromQuery] string username, [FromQuery] string roleName)
        {
            try
            {
                var success = await _roleRepository.AssignRoleToUserAsync(username, roleName);

                if (success)
                {
                    return Ok("Role assigned successfully.");
                }
                else
                {
                    return BadRequest("Failed to assign role.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }

        }


        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleRepository.GetAllRolesAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }




        [HttpGet("GetAllUsersWithRoles")]
        public async Task<IActionResult> GetAllUsersWithRoles()
        {
            try
            {
                var usersWithRoles = await _userRepository.GetAllUsersWithRolesAsync();
                return Ok(usersWithRoles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }


        [HttpPost("RemoveRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser([FromQuery] string username, [FromQuery] string roleName)
        {
            try
            {
                var success = await _roleRepository.RemoveRoleFromUserRolesAsync(username, roleName);
                if (success)
                {
                    return Ok(new { Message = "Role removed successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "User or Role not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
















    }
}
