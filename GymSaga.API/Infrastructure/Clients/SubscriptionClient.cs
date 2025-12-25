using Common.DTOs;
using System.Net.Http.Headers;

namespace GymSaga.API.Infrastructure.Clients
{
    public class SubscriptionsClient : ISubscriptionsClient
    {
        private readonly HttpClient _http;

        public SubscriptionsClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<Guid> CreateAsync(Guid memberId, RegisterMemberDto dto, Guid userId, string token)
        {
            dto.CreatedBy = userId;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var createDto = new CreateSubscriptionRequest
            {
                MemberId = memberId,
                PlanId = dto.PlanId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CreatedBy = dto.CreatedBy
            };

            var response = await _http.PostAsJsonAsync("api/subscriptions", createDto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CreateResultDto>();
            return result.Id;
        }

        public async Task CancelAsync(Guid subscriptionId)
        {
            var response = await _http.PostAsync($"api/subscriptions/{subscriptionId}/cancel", null);
            response.EnsureSuccessStatusCode();
        }
    }


}
