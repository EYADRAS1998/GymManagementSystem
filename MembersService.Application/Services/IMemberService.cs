using Common;
using MembersService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.Services
{
    public interface IMemberService
    {
        // Member Operations
        Task<MemberDto> GetByIdAsync(Guid id);
        Task<PagedResult<MemberDto>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Guid> CreateAsync(CreateMemberDto dto);
        Task UpdateAsync(Guid id, UpdateMemberDto dto);
        Task DeleteAsync(Guid id);

        // Statistics
        Task<int> GetTotalCountAsync();
        Task<int> GetActiveCountAsync();
    }
}
