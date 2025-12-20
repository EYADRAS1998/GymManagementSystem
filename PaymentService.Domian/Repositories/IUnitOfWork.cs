using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domian.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPaymentRepository Payments { get; }
        IPaymentInstallmentRepository PaymentInstallments { get; }

        Task<int> SaveChangesAsync();
    }
}
