using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.DTOs
{
    // لعرض الاشتراك
    public class SubscriptionReadDto
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
