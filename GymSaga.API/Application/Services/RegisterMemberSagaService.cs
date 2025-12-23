using Common.DTOs;
using GymSaga.API.Infrastructure.Clients;

namespace GymSaga.API.Application.Services
{
    public class RegisterMemberSagaService : IRegisterMemberSagaService
    {
        private readonly IMembersClient _membersClient;
        private readonly ISubscriptionsClient _subscriptionsClient;
        private readonly IPaymentsClient _paymentsClient;

        public RegisterMemberSagaService(
            IMembersClient membersClient,
            ISubscriptionsClient subscriptionsClient,
            IPaymentsClient paymentsClient)
        {
            _membersClient = membersClient;
            _subscriptionsClient = subscriptionsClient;
            _paymentsClient = paymentsClient;
        }

        public async Task<Guid> ExecuteAsync(RegisterMemberDto dto, Guid userId , string token)
        {
            Guid memberId = Guid.Empty;
            Guid subscriptionId = Guid.Empty;
            Guid paymentId = Guid.Empty;

            try
            {
                memberId = await _membersClient.CreateAsync(dto, userId, token);

                // 2️⃣ إنشاء الاشتراك للعضو
                subscriptionId = await _subscriptionsClient.CreateAsync(memberId, dto, userId, token);


                // إنشاء الدفع
                var paymentDto = new CreatePaymentRequest
                {
                    MemberId = memberId,
                    SubscriptionId = subscriptionId,
                    TotalAmount = dto.TotalAmount,
                    IsInstallment = dto.IsInstallment,
                    Currency = dto.Currency
                };
                paymentId = await _paymentsClient.CreateAsync(paymentDto, userId, token);

                return memberId;
            }
            catch
            {
                // Compensation
                if (subscriptionId != Guid.Empty)
                    await _subscriptionsClient.CancelAsync(subscriptionId);

                if (memberId != Guid.Empty)
                    await _membersClient.FreezeAsync(memberId, userId);

                throw;
            }
        }

    }
}
