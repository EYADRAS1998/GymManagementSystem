using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.DTOs.Payments
{
    public class PayInstallmentDto
    {
        public Guid InstallmentId { get; set; }
        public DateTime PaidAt { get; set; }
    }
}
