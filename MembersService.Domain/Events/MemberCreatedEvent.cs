using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Domain.Events
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
        DateTime CreatedAt
    );

}
