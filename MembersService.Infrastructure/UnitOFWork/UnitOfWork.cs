using MembersService.Domain.Repositories;
using MembersService.Infrastructure.Persistence;
using MembersService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Infrastructure.UnitOFWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Members = new MemberRepository(_context);
            MemberProgress = new MemberProgressRepository(_context);
        }

        public IMemberRepository Members { get; private set; }
        public IMemberProgressRepository MemberProgress { get; private set; }

        /// <summary>
        /// حفظ جميع التغييرات في قاعدة البيانات كوحدة واحدة
        /// </summary>
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// التراجع عن جميع التغييرات (في EF Core يتم عادة تلقائياً عند exception)
        /// </summary>
        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
                        break;
                }
            }
        }
    }
}
