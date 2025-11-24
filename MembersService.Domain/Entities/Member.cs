using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Domain.Entities
{
    public class Member
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }

        public Guid CreatedBy { get; set; }   // معرّف المستخدم الذي أضاف العضو
        public Guid? UpdatedBy { get; set; }  // معرّف آخر مستخدم قام بتحديث البيانات
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }


        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }  // "Male" / "Female"

        public DateTime JoinDate { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<MemberProgress> ProgressRecords { get; set; } = new List<MemberProgress>();
    }
}
