using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTOs.Payments;
using PaymentService.Application.Services.Abstractions;
using System.Security.Claims;

namespace PaymentService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaySubscriptionService _paySubscriptionService;

        public PaymentController(IPaySubscriptionService paySubscriptionService)
        {
            _paySubscriptionService = paySubscriptionService;
        }

        /// <summary>
        /// إنشاء دفع للاشتراك (نقدًا أو أقساط)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                                            // استخراج UserId من JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized();

            var createdBy = Guid.Parse(userIdClaim.Value);

            var paymentId = await _paySubscriptionService.CreatePaymentAsync(dto, createdBy);

            return CreatedAtAction(nameof(GetPaymentById), new { id = paymentId }, new { Id = paymentId });
        }

        /// <summary>
        /// دفع قسط
        /// </summary>
        [HttpPost("pay-installment")]
        public async Task<IActionResult> PayInstallment([FromBody] PayInstallmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // استخراج UserId من JWT
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized();

            var paidBy = Guid.Parse(userIdClaim.Value);

            await _paySubscriptionService.PayInstallmentAsync(dto, paidBy);

            return Ok();
        }

        /// <summary>
        /// استعراض الدفع بواسطة Id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            // هنا يمكنك إضافة دالة في الخدمة لإرجاع PaymentDto
            return Ok(); // مؤقتًا
        }
    }
}
