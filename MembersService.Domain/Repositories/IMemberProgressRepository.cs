using MembersService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Domain.Repositories
{
    public interface IMemberProgressRepository
    {
        Task<MemberProgress?> GetByIdAsync(Guid id);

        Task<IEnumerable<MemberProgress>> GetByMemberIdAsync(Guid memberId);

        Task AddAsync(MemberProgress progress);
        Task UpdateAsync(MemberProgress progress);
        Task DeleteAsync(Guid id);
    }
}
