using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.DTOs
{
    public class PlanUpdateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
