using Common;
using MembersService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Domain.Repositories
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(Guid id);
        Task<PagedResult<Member>> GetPagedAsync(int pageNumber, int pageSize);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(Guid id);
        // ======================
        // Activation / Freezing
        // ======================
        Task FreezeAsync(Guid memberId, Guid updatedBy);
        Task ActivateAsync(Guid memberId, Guid updatedBy);

        // ======================
        // Statistics
        // ======================
        Task<int> GetTotalCountAsync();
        Task<int> GetActiveCountAsync();

        Task<bool> ExistsAsync(Guid id);

    }
}
