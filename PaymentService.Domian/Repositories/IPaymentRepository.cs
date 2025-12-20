using PaymentService.Domian.Entities;

namespace PaymentService.Domian.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(Guid id);

        Task<Payment?> GetBySubscriptionIdAsync(Guid subscriptionId);

        Task<IEnumerable<Payment>> GetByMemberIdAsync(Guid memberId);

        Task AddAsync(Payment payment);

        void Update(Payment payment);
    }
}
