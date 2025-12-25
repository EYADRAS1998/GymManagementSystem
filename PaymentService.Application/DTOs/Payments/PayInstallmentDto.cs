using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs.Payments
{
    public class PayInstallmentDto
    {
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string ReferenceNumber { get; set; } = string.Empty;
    }
}
