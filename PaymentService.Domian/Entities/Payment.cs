using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domian.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid SubscriptionId { get; set; } // مرجع للاشتراك
        public Guid MemberId { get; set; }       // مرجع للعضو

        public decimal TotalAmount { get; set; } // المبلغ الكلي
        public string Currency { get; set; }     // USD, EUR, SAR ...

        public PaymentType PaymentType { get; set; } // Cash | Installments
        public PaymentStatus Status { get; set; }    // Pending | Paid | PartiallyPaid

        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
