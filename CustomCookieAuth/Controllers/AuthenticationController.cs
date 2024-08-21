using CustomCookieAuth.Models;
using CustomCookieAuth.Services; 
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace CustomCookieAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationUserService _applicationUserService;

        public AuthenticationController(ApplicationUserService applicationUserService)
        {
            _applicationUserService = applicationUserService;
        }

        // Register route: POST /api/Authentication/register
        [HttpPost("register")]
        public async Task<IActionResult> registerUser([FromBody] RegisterDTO dto)
        {
            return Ok(await _applicationUserService.RegisterApplicationUser(dto));
        }

        // Login route: POST /api/Authentication/login
        [HttpPost("login")]
        public async Task<IActionResult> loginUser([FromBody] LoginDTO dto)
        {
            if (!await _applicationUserService.ValidateUser(dto))
            {
                return BadRequest();
            }

            var (claims, cookie) = await _applicationUserService.LoginUser(dto);
            Response.Cookies.Append("AuthCookie", JsonSerializer.Serialize(claims), cookie);
            return Ok("Login successful: " + claims.Role);
        }

        // Logout route: POST /api/Authentication/logout
        [HttpPost("logout")]
        public async Task<IActionResult> logout()
        {
            Response.Cookies.Delete("AuthCookie");
            return Ok("Logout successful");
        }
    }
}
