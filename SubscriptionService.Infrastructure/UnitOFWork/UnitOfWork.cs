using SubscriptionService.Domain.Repositories;
using SubscriptionService.Infrastructure.Persistence;
using SubscriptionService.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Infrastructure.UnitOFWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Subscriptions = new SubscriptionRepository(_context);
            Plans = new PlanRepository(_context);
        }

        public ISubscriptionRepository Subscriptions { get; private set; }
        public IPlanRepository Plans { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
