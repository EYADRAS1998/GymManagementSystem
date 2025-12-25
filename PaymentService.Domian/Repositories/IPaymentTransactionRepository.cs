using PaymentService.Domian.Entities;

namespace PaymentService.Domian.Repositories
{
    public interface IPaymentTransactionRepository
    {
        Task AddAsync(PaymentTransaction transaction);

        Task<PaymentTransaction?> GetByIdAsync(Guid id);

        Task<IEnumerable<PaymentTransaction>> GetByPaymentIdAsync(Guid paymentId);

        Task<IEnumerable<PaymentTransaction>> GetByInstallmentIdAsync(Guid installmentId);

        Task<decimal> GetTotalPaidAmountAsync(Guid paymentId);
    }
}
