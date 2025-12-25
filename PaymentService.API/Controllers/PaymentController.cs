using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.DTOs.Payments;
using PaymentService.Application.Services.Abstractions;

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

        // POST: api/Payment
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? Guid.NewGuid().ToString());
            var paymentId = await _paySubscriptionService.CreatePaymentAsync(dto, userId);
            return Ok(new { PaymentId = paymentId });
        }

        // POST: api/Payment/{paymentId}/Installments/{installmentId}/Pay
        [HttpPost("{paymentId}/Installments/{installmentId}/Pay")]
        public async Task<IActionResult> PayInstallment(
            Guid paymentId,
            Guid installmentId,
            [FromBody] PayInstallmentDto dto)
        {
            var transactionId = await _paySubscriptionService.PayInstallmentAsync(
                paymentId,
                installmentId,
                dto.Amount,
                dto.Method,
                dto.ReferenceNumber
            );

            return Ok(new { TransactionId = transactionId });
        }

        // GET: api/Payment/{paymentId}
        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentDetails(Guid paymentId)
        {
            var details = await _paySubscriptionService.GetPaymentDetailsAsync(paymentId);
            return Ok(details);
        }

        // GET: api/Payment/PendingInstallments/{memberId}
        [HttpGet("PendingInstallments/{memberId}")]
        public async Task<IActionResult> GetPendingInstallments(Guid memberId)
        {
            var installments = await _paySubscriptionService.GetPendingInstallmentsAsync(memberId);
            return Ok(installments);
        }

        // GET: api/Payment/{paymentId}/Transactions
        [HttpGet("{paymentId}/Transactions")]
        public async Task<IActionResult> GetPaymentTransactions(Guid paymentId)
        {
            var transactions = await _paySubscriptionService.GetPaymentTransactionsAsync(paymentId);
            return Ok(transactions);
        }
    }
}
