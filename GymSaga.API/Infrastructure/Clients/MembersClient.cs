using Common.DTOs;

namespace GymSaga.API.Infrastructure.Clients
{
    public class MembersClient : IMembersClient
    {
        private readonly HttpClient _http;

        public MembersClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<Guid> CreateAsync(RegisterMemberDto dto, Guid userId, string token)
        {
            dto.CreatedBy = userId; // تمرير UserId
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await _http.PostAsJsonAsync("api/members/register", dto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CreateResultDto>();
            return result.Id;
        }

        public async Task FreezeAsync(Guid memberId, Guid updatedBy)
        {
            var response = await _http.PostAsync($"api/members/{memberId}/freeze", null);
            response.EnsureSuccessStatusCode();
        }

        public async Task ActivateAsync(Guid memberId, Guid updatedBy)
        {
            var response = await _http.PostAsync($"api/members/{memberId}/activate", null);
            response.EnsureSuccessStatusCode();
        }
    }

    public class CreateResultDto
    {
        public Guid Id { get; set; }
    }
}
