using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.DTOs
{
    public class MemberDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public DateTime JoinDate { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
