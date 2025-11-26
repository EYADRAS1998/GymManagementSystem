using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Application.DTOs;
using SubscriptionService.Application.Services;

namespace SubscriptionService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _planService.GetAllAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var plan = await _planService.GetByIdAsync(id);
            return Ok(plan);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanCreateDto dto)
        {
            var plan = await _planService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlanUpdateDto dto)
        {
            var plan = await _planService.UpdateAsync(id, dto);
            return Ok(plan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _planService.DeleteAsync(id);
            return NoContent();
        }
    }
}
