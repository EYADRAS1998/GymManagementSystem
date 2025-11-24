using MembersService.Application.DTOs;
using MembersService.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MembersService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var member = await _memberService.GetByIdAsync(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(int pageNumber = 1, int pageSize = 10)
        {
            var pagedMembers = await _memberService.GetPagedAsync(pageNumber, pageSize);
            return Ok(pagedMembers);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMemberDto dto)
        {
            // استخراج UserId من JWT
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("UserId claim is missing in the token.");

            dto.CreatedBy = Guid.Parse(userIdClaim);

            var memberId = await _memberService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = memberId }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMemberDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("UserId claim is missing in the token.");

            dto.UpdatedBy = Guid.Parse(userIdClaim);

            await _memberService.UpdateAsync(id, dto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _memberService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("statistics/total")]
        public async Task<IActionResult> GetTotalCount()
        {
            var total = await _memberService.GetTotalCountAsync();
            return Ok(total);
        }

        [HttpGet("statistics/active")]
        public async Task<IActionResult> GetActiveCount()
        {
            var active = await _memberService.GetActiveCountAsync();
            return Ok(active);
        }
    }
}
