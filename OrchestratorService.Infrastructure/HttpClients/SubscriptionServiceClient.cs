using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrchestratorService.Infrastructure.HttpClients
{
    public class SubscriptionServiceClient : ISubscriptionServiceClient
    {
        private readonly HttpClient _httpClient;

        public SubscriptionServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> CreateSubscriptionAsync(object subscriptionDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(subscriptionDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Subscriptions", content);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<JsonElement>(responseData);
            return data.GetProperty("id").GetGuid();
        }
    }
}
