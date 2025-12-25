using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs.Payments
{
    public class PaymentTransactionDto
    {
        public Guid Id { get; set; }

        public Guid PaymentId { get; set; }
        public Guid? PaymentInstallmentId { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public PaymentMethod Method { get; set; }
        public TransactionStatus Status { get; set; }

        public DateTime PaidAt { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
