using Common;
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
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Member member)
        {
            await _context.Members.AddAsync(member);
        }

        public async Task DeleteAsync(Guid id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Members.AnyAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members
                .Include(m => m.ProgressRecords)
                .ToListAsync();
        }

        public async Task<Member?> GetByIdAsync(Guid id)
        {
            return await _context.Members
                .Include(m => m.ProgressRecords)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedResult<Member>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var total = await _context.Members.CountAsync();

            var items = await _context.Members
                .OrderBy(m => m.FullName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Member>(items, total);
        }

        public async Task<int> GetActiveCountAsync()
        {
            return await _context.Members.CountAsync(m => m.IsActive);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Members.CountAsync();
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
        }
    }
}
