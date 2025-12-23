using Common.DTOs;

namespace GymSaga.API.Infrastructure.Clients
{
    public class PaymentsClient : IPaymentsClient
    {
        private readonly HttpClient _http;

        public PaymentsClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<Guid> CreateAsync(CreatePaymentRequest dto, Guid userId , string token)
        {
            dto.CreatedBy = userId;

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _http.PostAsJsonAsync("api/payment", dto);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<CreateResultDto>();
            return result.Id;
        }
    }


}
