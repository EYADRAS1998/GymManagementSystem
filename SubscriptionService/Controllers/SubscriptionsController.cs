using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Services;
using System.Security.Claims;

namespace SubscriptionService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var subscriptions = await _subscriptionService.GetAllAsync(pageNumber, pageSize);
            return Ok(subscriptions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subscription = await _subscriptionService.GetByIdAsync(id);
            return Ok(subscription);
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMemberId(Guid memberId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var subscriptions = await _subscriptionService.GetByMemberIdAsync(memberId, pageNumber, pageSize);
            return Ok(subscriptions);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubscriptionCreateDto dto)
        {
            // استخراج UserId من JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized();

            var userId = Guid.Parse(userIdClaim.Value);

            var subscription = await _subscriptionService.CreateAsync(dto, userId);
            return CreatedAtAction(nameof(GetById), new { id = subscription.Id }, subscription);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SubscriptionUpdateDto dto)
        {
            var subscription = await _subscriptionService.UpdateAsync(id, dto);
            return Ok(subscription);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _subscriptionService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _subscriptionService.CancelAsync(id);
            return NoContent();
        }

    }
}
