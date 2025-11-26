using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.DTOs
{
    // لعرض الاشتراكات مع Pagination
    public class SubscriptionPagedDto
    {
        public IEnumerable<SubscriptionReadDto> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
