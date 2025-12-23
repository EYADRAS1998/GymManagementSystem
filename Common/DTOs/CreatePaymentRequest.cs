using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class CreatePaymentRequest
    {
        public Guid MemberId { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid CreatedBy { get; set; }

        public decimal TotalAmount { get; set; }
        public bool IsInstallment { get; set; } = false;

        public string Currency { get; set; } = "USD";
    }
}
