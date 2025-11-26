using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.DTOs
{
    public class SubscriptionUpdateDto
    {
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
