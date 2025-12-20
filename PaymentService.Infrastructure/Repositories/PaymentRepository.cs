using Microsoft.EntityFrameworkCore;
using PaymentService.Domian.Entities;
using PaymentService.Domian.Repositories;
using PaymentService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<Payment?> GetByIdAsync(Guid id)
        {
            return await _context.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment?> GetBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.SubscriptionId == subscriptionId);
        }

        public async Task<IEnumerable<Payment>> GetByMemberIdAsync(Guid memberId)
        {
            return await _context.Payments
                .Where(p => p.MemberId == memberId)
                .ToListAsync();
        }

        public void Update(Payment payment)
        {
            _context.Payments.Update(payment);
        }
    }
}
