using Common.Events;
using Common.Messaging;
using PaymentService.Application.DTOs.Payments;
using PaymentService.Application.Services.Abstractions;
using PaymentService.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.EventHandlers
{
    public class SubscriptionCreatedEventHandler : IEventHandler<SubscriptionCreatedEvent>
    {
        private readonly IPaySubscriptionService _paySubscriptionService;

        public SubscriptionCreatedEventHandler(IPaySubscriptionService paySubscriptionService)
        {
            _paySubscriptionService = paySubscriptionService;
        }

        public async Task HandleAsync(SubscriptionCreatedEvent @event)
        {
            // إعداد CreatePaymentDto بناءً على بيانات الحدث
            var paymentDto = new CreatePaymentDto
            {
                MemberId = @event.MemberId,
                SubscriptionId = @event.SubscriptionId,
                TotalAmount = @event.Amount,
                Currency = @event.Currency,
                PaymentType = @event.IsInstallment ? PaymentType.Installments : PaymentType.Cash,
                Installments = null
            };

            // إذا الدفع بالتقسيط، قم بإنشاء أقساط وفق منطق PaySubscriptionService
            if (@event.IsInstallment)
            {
               
            }

            // إنشاء الدفعة باستخدام الخدمة التي تتحقق من كل القواعد
            await _paySubscriptionService.CreatePaymentAsync(paymentDto, Guid.Empty); // يمكن وضع CreatedBy إذا متاح
        }
    }
}