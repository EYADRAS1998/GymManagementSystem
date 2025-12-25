using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs.Payments
{
    public class CreatePaymentDto
    {
        public Guid SubscriptionId { get; set; }
        public Guid MemberId { get; set; }

        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }

        public PaymentType PaymentType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        // تُستخدم فقط في حالة Installments
        public List<CreatePaymentInstallmentDto>? Installments { get; set; }
    }
}
