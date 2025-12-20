using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs.Payments
{
    public class PaymentDto
    {
        public Guid Id { get; set; }

        public Guid SubscriptionId { get; set; }
        public Guid MemberId { get; set; }

        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }

        public PaymentType PaymentType { get; set; }
        public PaymentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<PaymentInstallmentDto>? Installments { get; set; }
    }
}
