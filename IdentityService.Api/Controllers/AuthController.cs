using Microsoft.AspNetCore.Mvc;
using IdentityService.Application.DTOs;
using IdentityService.Application.Services;

namespace IdentityService.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto.UserNameOrEmail, dto.Password);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
        {
            var token = await _authService.RegisterAsync(dto);
            return Ok(new { Token = token });
        }
    }

}
