using PaymentService.Domian.Entities;

namespace PaymentService.Domian.Repositories
{
    public interface IPaymentInstallmentRepository
    {
        Task<PaymentInstallment?> GetByIdAsync(Guid id);

        Task<IEnumerable<PaymentInstallment>> GetByPaymentIdAsync(Guid paymentId);

        Task<IEnumerable<PaymentInstallment>> GetUnpaidInstallmentsAsync(Guid paymentId);

        Task AddAsync(PaymentInstallment installment);

        void Update(PaymentInstallment installment);
    }
}
