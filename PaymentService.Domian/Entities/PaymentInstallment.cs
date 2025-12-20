using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domian.Entities
{
    public class PaymentInstallment
    {
        public Guid Id { get; set; }

        public Guid PaymentId { get; set; }

        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }     // تاريخ الاستحقاق
        public DateTime? PaidAt { get; set; }     // وقت الدفع الفعلي

        public InstallmentStatus Status { get; set; } // Pending | Paid
    }
}
