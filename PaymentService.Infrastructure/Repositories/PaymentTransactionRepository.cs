using Microsoft.EntityFrameworkCore;
using PaymentService.Domian.Entities;
using PaymentService.Domian.Enums;
using PaymentService.Domian.Repositories;
using PaymentService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Repositories
{
    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentTransactionRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PaymentTransaction transaction)
        {
            await _context.PaymentTransactions.AddAsync(transaction);
        }

        public async Task<PaymentTransaction?> GetByIdAsync(Guid id)
        {
            return await _context.PaymentTransactions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PaymentTransaction>> GetByPaymentIdAsync(Guid paymentId)
        {
            return await _context.PaymentTransactions
                .AsNoTracking()
                .Where(x => x.PaymentId == paymentId)
                .OrderBy(x => x.PaidAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<PaymentTransaction>> GetByInstallmentIdAsync(Guid installmentId)
        {
            return await _context.PaymentTransactions
                .AsNoTracking()
                .Where(x => x.PaymentInstallmentId == installmentId)
                .OrderBy(x => x.PaidAt)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalPaidAmountAsync(Guid paymentId)
        {
            return await _context.PaymentTransactions
                .Where(x => x.PaymentId == paymentId && x.Status == TransactionStatus.Success)
                .SumAsync(x => x.Amount);
        }
    }
}
