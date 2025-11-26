using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Domain.Entities
{
    public class Plan
    {
        public Guid Id { get; set; }         // معرف الخطة
        public string Name { get; set; }     // اسم الخطة
        public decimal Price { get; set; }   // سعر الخطة
    }
}
