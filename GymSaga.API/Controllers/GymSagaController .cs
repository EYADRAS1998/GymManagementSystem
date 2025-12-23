using Common.DTOs;
using GymSaga.API.Application.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymSaga.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GymSagaController : ControllerBase
    {
        private readonly IRegisterMemberSagaService _sagaService;

        public GymSagaController(IRegisterMemberSagaService sagaService)
        {
            _sagaService = sagaService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterMemberDto dto)
        {
            // استخراج UserId من JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("UserId");
            var token = await HttpContext.GetTokenAsync("access_token");

            if (userIdClaim == null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            // تمرير UserId إلى الـ Saga
            var memberId = await _sagaService.ExecuteAsync(dto, userId, token);

            return CreatedAtAction(null, new { Id = memberId }, new { Id = memberId });
        }
    }

}
