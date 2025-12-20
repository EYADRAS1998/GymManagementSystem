using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrchestratorService.Infrastructure.HttpClients
{
    public class MembersServiceClient : IMembersServiceClient
    {
        private readonly HttpClient _httpClient;

        public MembersServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> CreateMemberAsync(object memberDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(memberDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Members", content);
            response.EnsureSuccessStatusCode();

            var location = response.Headers.Location;
            var idString = location?.Segments[^1];
            return Guid.Parse(idString!);
        }

        public async Task<bool> MemberExistsAsync(Guid memberId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"api/Members/{memberId}");
            return response.IsSuccessStatusCode;
        }
    }
}
