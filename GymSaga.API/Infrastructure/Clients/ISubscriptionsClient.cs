using Common.DTOs;

namespace GymSaga.API.Infrastructure.Clients
{
    public interface ISubscriptionsClient
    {
        Task<Guid> CreateAsync(Guid memberId, RegisterMemberDto dto, Guid userId, string token);
        Task CancelAsync(Guid subscriptionId);
    }
}
