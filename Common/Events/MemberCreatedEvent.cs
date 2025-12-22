using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Events
{
    public record MemberCreatedEvent(
        Guid MemberId,
        string FullName,
        string PhoneNumber,
        string? Email,
        DateTime BirthDate,
        string Gender,
        string? Notes,
        Guid CreatedBy,
        DateTime CreatedAt,

        // تفاصيل الاشتراك المدخلة من المستخدم
        Guid PlanId,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive,

        // تفاصيل الدفع
        decimal TotalAmount,
        bool IsInstallment,
        string Currency
    );

}
