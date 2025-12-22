using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Events
{
    public record SubscriptionCreatedEvent(
        Guid SubscriptionId,
        Guid MemberId,
        Guid PlanId,
        decimal Amount,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive,
        bool IsInstallment,
        string Currency
    );
}
