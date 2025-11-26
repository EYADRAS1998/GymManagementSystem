using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }              // معرف الاشتراك
        public Guid MemberId { get; set; }        // معرف العضو من MembersService
        public Guid PlanId { get; set; }          // معرف الخطة
        public DateTime StartDate { get; set; }   // بداية الاشتراك
        public DateTime EndDate { get; set; }     // نهاية الاشتراك
        public bool IsActive { get; set; }        // حالة الاشتراك
        public Guid CreatedBy { get; set; }       // معرف المستخدم الذي أنشأ الاشتراك (UserId من IdentityService)
        public DateTime CreatedAt { get; set; }   // وقت إنشاء السجل
    }
}
