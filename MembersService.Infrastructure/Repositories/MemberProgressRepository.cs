using MembersService.Domain.Entities;
using MembersService.Domain.Repositories;
using MembersService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Infrastructure.Repositories
{
    public class MemberProgressRepository : IMemberProgressRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(MemberProgress progress)
        {
            await _context.MemberProgresses.AddAsync(progress);
        }

        public async Task DeleteAsync(Guid id)
        {
            var progress = await _context.MemberProgresses.FindAsync(id);
            if (progress != null)
            {
                _context.MemberProgresses.Remove(progress);
            }
        }

        public async Task<MemberProgress?> GetByIdAsync(Guid id)
        {
            return await _context.MemberProgresses
                .Include(p => p.Member)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<MemberProgress>> GetByMemberIdAsync(Guid memberId)
        {
            return await _context.MemberProgresses
                .Where(p => p.MemberId == memberId)
                .OrderByDescending(p => p.DateRecorded)
                .ToListAsync();
        }

        public async Task UpdateAsync(MemberProgress progress)
        {
            _context.MemberProgresses.Update(progress);
        }
    }
}
