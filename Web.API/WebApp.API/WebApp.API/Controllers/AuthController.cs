
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthenticationService _authenticationService;

        public AuthController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }





        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
         
            if (request == null)
            {
                return BadRequest(new { Message = "Request body cannot be null." });
            }

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email) ||
                string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Username, email, and password are required." });
            }

            try
            {
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };

                var token = await _authenticationService.RegisterAsync(user, request.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }



        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
           

            if (request == null)
            {
                return BadRequest(new { Message = "Request body cannot be null." });
            }

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "Username and password are required." });
            }

            try
            {
                var token = await _authenticationService.LoginAsync(request.Username, request.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }


        }



    }
}
