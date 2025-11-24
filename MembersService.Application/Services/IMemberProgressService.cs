using MembersService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.Services
{
    public interface IMemberProgressService
    {
        Task<MemberProgressDto> GetByIdAsync(Guid id);
        Task<IEnumerable<MemberProgressDto>> GetByMemberIdAsync(Guid memberId);
        Task<Guid> CreateAsync(CreateMemberProgressDto dto);
        Task UpdateAsync(Guid id, UpdateMemberProgressDto dto);
        Task DeleteAsync(Guid id);
    }
}
