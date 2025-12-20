using PaymentService.Application.DTOs.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Services.Abstractions
{
    public interface IPaySubscriptionService
    {
        Task<Guid> CreatePaymentAsync(CreatePaymentDto dto, Guid createdBy);

        Task PayInstallmentAsync(PayInstallmentDto dto, Guid paidBy);
    }
}
