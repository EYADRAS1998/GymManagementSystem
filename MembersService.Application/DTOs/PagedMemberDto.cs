using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.DTOs
{
    public class PagedMemberDto : PagedResult<MemberDto>
    {
        public PagedMemberDto(IEnumerable<MemberDto> items, int totalCount)
            : base(items, totalCount) { }
    }
}
