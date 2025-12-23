using Common;
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Domain.Entities;
using SubscriptionService.Domain.Repositories;
using SubscriptionService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
        }

        public void Delete(Subscription subscription)
        {
            _context.Subscriptions.Remove(subscription);
        }

        public async Task<Subscription> GetByIdAsync(Guid id)
        {
            return await _context.Subscriptions.FindAsync(id);
        }

        public async Task<PagedResult<Subscription>> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Subscriptions.CountAsync();
            var items = await _context.Subscriptions
                .OrderBy(s => s.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Subscription>(items, totalCount);
        }

        public async Task<PagedResult<Subscription>> GetByMemberIdAsync(Guid memberId, int pageNumber, int pageSize)
        {
            var query = _context.Subscriptions.Where(s => s.MemberId == memberId);
            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Subscription>(items, totalCount);
        }

        public void Update(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
        }
        public async Task CancelAsync(Guid subscriptionId)
        {
            var subscription = await _context.Subscriptions.FindAsync(subscriptionId);

            if (subscription == null)
                throw new Exception("Subscription not found");

            subscription.Cancel();
        }


    }
}
