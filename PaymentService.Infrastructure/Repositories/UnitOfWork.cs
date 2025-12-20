using PaymentService.Domian.Repositories;
using PaymentService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentDbContext _context;

        private IPaymentRepository? _paymentRepository;
        private IPaymentInstallmentRepository? _paymentInstallmentRepository;

        public UnitOfWork(PaymentDbContext context)
        {
            _context = context;
        }

        public IPaymentRepository Payments
            => _paymentRepository ??= new PaymentRepository(_context);

        public IPaymentInstallmentRepository PaymentInstallments
            => _paymentInstallmentRepository ??= new PaymentInstallmentRepository(_context);

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
