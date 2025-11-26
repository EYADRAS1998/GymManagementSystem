using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.DTOs
{
    public class SubscriptionCreateDto
    {
        public Guid MemberId { get; set; }    // معرف العضو من MembersService
        public Guid PlanId { get; set; }      // معرف الخطة
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
