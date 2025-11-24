using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MembersService.Application.DTOs
{
    public class MemberProgressDto
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public decimal Weight { get; set; }
        public string MeasurementsJson { get; set; }
        public string? Notes { get; set; }
        public DateTime DateRecorded { get; set; }
        public Guid RecordedBy { get; set; }
    }
}
