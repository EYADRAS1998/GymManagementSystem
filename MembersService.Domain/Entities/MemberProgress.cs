using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Domain.Entities
{
    public class MemberProgress
    {
        public Guid Id { get; set; }

        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        public decimal Weight { get; set; }  // e.g. 70.5 kg

        public Guid RecordedBy { get; set; }   // معرّف المدرب أو المستخدم الذي سجل التقدم
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;



        // JSON string to store measurements like:
        // { "Chest": 102, "Arm": 36, "Waist": 88 }
        public string MeasurementsJson { get; set; }

        public string? Notes { get; set; }

        public DateTime DateRecorded { get; set; } = DateTime.UtcNow;
    }
}
