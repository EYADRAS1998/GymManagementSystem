using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs.Payments
{
    public class CreatePaymentInstallmentDto
    {
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
    }
}
