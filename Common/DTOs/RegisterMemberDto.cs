using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class RegisterMemberDto
    {
        // بيانات العضو
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = null!; // "Male" / "Female"
        public string? Notes { get; set; }

        // بيانات الاشتراك
        public Guid PlanId { get; set; }           // معرف الخطة المحددة في النظام
        public DateTime StartDate { get; set; }    // تاريخ بداية الاشتراك
        public DateTime EndDate { get; set; }      // تاريخ نهاية الاشتراك
        public bool IsActive { get; set; } = true; // حالة الاشتراك بعد الإنشاء

        // بيانات الدفع
        public decimal TotalAmount { get; set; }   // قيمة الاشتراك
        public bool IsInstallment { get; set; }    // true إذا الدفع بالتقسيط، false للدفع كامل
        public string Currency { get; set; } = "USD"; // العملة الافتراضية

        // معلومات المستخدم الذي أضاف العضو
        public Guid CreatedBy { get; set; }
    }


}
