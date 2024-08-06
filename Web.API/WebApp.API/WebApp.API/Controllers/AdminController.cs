﻿using Microsoft.AspNetCore.Authorization;
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

        public AdminController(IRoleRepository rolerepository)
        {
            _roleRepository = rolerepository;
             
        }

        [HttpPost("SeedRoles")]
       
        public async Task<IActionResult> SeedRoles()
        {
            try
            {
                // Assuming _roleSeeder is injected in the controller
                await _roleRepository.SeedRolesAsync();
                return Ok("Roles seeded successfully");
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

    }
}
