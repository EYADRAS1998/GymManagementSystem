using OrchestratorService.Application.DTOs;
using OrchestratorService.Infrastructure.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorService.Application.Services
{
    public class MemberSubscriptionOrchestrator : IMemberSubscriptionOrchestrator
    {
        private readonly IMembersServiceClient _membersClient;
        private readonly ISubscriptionServiceClient _subscriptionsClient;

        public MemberSubscriptionOrchestrator(
            IMembersServiceClient membersClient,
            ISubscriptionServiceClient subscriptionsClient)
        {
            _membersClient = membersClient;
            _subscriptionsClient = subscriptionsClient;
        }

        public async Task<Guid> AddMemberWithSubscriptionAsync(AddMemberWithSubscriptionDto dto, string token)
        {
            // تحويل DTO ليطابق MembersService DTO format
            var memberDto = new
            {
                dto.FullName,
                dto.PhoneNumber,
                dto.Email,
                dto.BirthDate,
                dto.Gender,
                dto.Notes
            };

            var memberId = await _membersClient.CreateMemberAsync(memberDto, token);

            var subscriptionDto = new
            {
                MemberId = memberId,
                dto.PlanId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1)
            };

            var subscriptionId = await _subscriptionsClient.CreateSubscriptionAsync(subscriptionDto, token);

            return subscriptionId;
        }

        public async Task<Guid> RenewSubscriptionAsync(RenewSubscriptionDto dto, string token)
        {
            var exists = await _membersClient.MemberExistsAsync(dto.MemberId, token);
            if (!exists)
                throw new Exception("Member does not exist");

            var subscriptionDto = new
            {
                dto.MemberId,
                dto.PlanId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1)
            };

            var subscriptionId = await _subscriptionsClient.CreateSubscriptionAsync(subscriptionDto, token);

            return subscriptionId;
        }
    }
}
