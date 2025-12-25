using PaymentService.Application.DTOs.Payments;
using PaymentService.Domian.Enums;

namespace PaymentService.Application.Services.Abstractions
{
    public interface IPaySubscriptionService
    {
        // إنشاء اشتراك مع دفع كامل أو دفعة أولى
        Task<Guid> CreatePaymentAsync(CreatePaymentDto dto, Guid createdBy);

        // دفع قسط محدد
        Task<Guid> PayInstallmentAsync(Guid paymentId, Guid installmentId, decimal amount, PaymentMethod method, string referenceNumber);

        // تفاصيل الدفع مع الأقساط
        Task<PaymentDetailsDto> GetPaymentDetailsAsync(Guid paymentId);

        // قائمة الأقساط غير المدفوعة
        Task<IEnumerable<PaymentInstallmentDto>> GetPendingInstallmentsAsync(Guid memberId);

        // كشف جميع المعاملات المتعلقة بالدفع
        Task<IEnumerable<PaymentTransactionDto>> GetPaymentTransactionsAsync(Guid paymentId);
    }
}
