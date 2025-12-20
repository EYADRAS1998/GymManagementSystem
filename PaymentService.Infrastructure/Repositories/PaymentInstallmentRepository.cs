using Microsoft.EntityFrameworkCore;
using PaymentService.Domian.Entities;
using PaymentService.Domian.Repositories;
using PaymentService.Infrastructure.Persistence;

namespace PaymentService.Infrastructure.Repositories
{
    public class PaymentInstallmentRepository : IPaymentInstallmentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentInstallmentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PaymentInstallment installment)
        {
            await _context.PaymentInstallments.AddAsync(installment);
        }

        public async Task<PaymentInstallment?> GetByIdAsync(Guid id)
        {
            return await _context.PaymentInstallments
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<PaymentInstallment>> GetByPaymentIdAsync(Guid paymentId)
        {
            return await _context.PaymentInstallments
                .Where(i => i.PaymentId == paymentId)
                .OrderBy(i => i.DueDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<PaymentInstallment>> GetUnpaidInstallmentsAsync(Guid paymentId)
        {
            return await _context.PaymentInstallments
                .Where(i => i.PaymentId == paymentId && i.PaidAt == null)
                .ToListAsync();
        }

        public void Update(PaymentInstallment installment)
        {
            _context.PaymentInstallments.Update(installment);
        }
    }
}
