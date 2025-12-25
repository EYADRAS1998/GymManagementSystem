using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TransactionStatus = PaymentService.Domian.Enums.TransactionStatus;

namespace PaymentService.Domian.Entities
{
    public class PaymentTransaction
    {
        public Guid Id { get; set; }

        public Guid PaymentId { get; set; }
        public Guid? PaymentInstallmentId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public PaymentMethod Method { get; set; } // Cash | Card | Transfer
        public TransactionStatus Status { get; set; } // Success | Failed

        public DateTime PaidAt { get; set; }

        public string ReferenceNumber { get; set; }
    }
}
