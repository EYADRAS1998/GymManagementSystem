using Common.DTOs;

namespace GymSaga.API.Infrastructure.Clients
{
    public interface IPaymentsClient
    {
        Task<Guid> CreateAsync(CreatePaymentRequest dto, Guid userId, string token);
    }
}
