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
    public class PlanRepository : IPlanRepository
    {
        private readonly ApplicationDbContext _context;

        public PlanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Plan plan)
        {
            await _context.Plans.AddAsync(plan);
        }

        public void Delete(Plan plan)
        {
            _context.Plans.Remove(plan);
        }

        public async Task<IEnumerable<Plan>> GetAllAsync()
        {
            return await _context.Plans.ToListAsync();
        }

        public async Task<Plan> GetByIdAsync(Guid id)
        {
            return await _context.Plans.FindAsync(id);
        }

        public void Update(Plan plan)
        {
            _context.Plans.Update(plan);
        }
    }
}
