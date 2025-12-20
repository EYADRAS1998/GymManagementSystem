using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Domian.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        PartiallyPaid = 2,
        Paid = 3
    }
}
