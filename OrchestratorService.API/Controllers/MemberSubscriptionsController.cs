using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrchestratorService.Application.DTOs;
using OrchestratorService.Application.Services;

namespace OrchestratorService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // يضمن وجود التوكن
    public class MemberSubscriptionsController : ControllerBase
    {
        private readonly IMemberSubscriptionOrchestrator _orchestrator;

        public MemberSubscriptionsController(IMemberSubscriptionOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        private string GetToken()
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authHeader))
                throw new UnauthorizedAccessException("Authorization header is missing");

            return authHeader.Replace("Bearer ", "");
        }

        [HttpPost("add-with-subscription")]
        public async Task<IActionResult> AddMemberWithSubscription([FromBody] AddMemberWithSubscriptionDto dto)
        {
            var token = GetToken();
            var subscriptionId = await _orchestrator.AddMemberWithSubscriptionAsync(dto, token);

            return Ok(new { SubscriptionId = subscriptionId });
        }

        [HttpPost("renew")]
        public async Task<IActionResult> RenewSubscription([FromBody] RenewSubscriptionDto dto)
        {
            var token = GetToken();
            var subscriptionId = await _orchestrator.RenewSubscriptionAsync(dto, token);

            return Ok(new { SubscriptionId = subscriptionId });
        }
    }
}
