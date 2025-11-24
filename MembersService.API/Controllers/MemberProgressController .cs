using MembersService.Application.DTOs;
using MembersService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MembersService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberProgressController : ControllerBase
    {
        private readonly IMemberProgressService _progressService;

        public MemberProgressController(IMemberProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var progress = await _progressService.GetByIdAsync(id);
            if (progress == null)
                return NotFound();

            return Ok(progress);
        }

        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMemberId(Guid memberId)
        {
            var progresses = await _progressService.GetByMemberIdAsync(memberId);
            return Ok(progresses);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMemberProgressDto dto)
        {
            // استخراج UserId من JWT
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("UserId claim is missing in the token.");

            dto.RecordedBy = Guid.Parse(userIdClaim);

            var id = await _progressService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMemberProgressDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("UserId claim is missing in the token.");

            dto.RecordedBy = Guid.Parse(userIdClaim);

            await _progressService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _progressService.DeleteAsync(id);
            return NoContent();
        }
    }
}
