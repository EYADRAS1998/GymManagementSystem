using Common.DTOs;

namespace GymSaga.API.Infrastructure.Clients
{
    public interface IMembersClient
    {
        Task<Guid> CreateAsync(RegisterMemberDto dto, Guid userId, string token);
        Task FreezeAsync(Guid memberId, Guid updatedBy);
        Task ActivateAsync(Guid memberId, Guid updatedBy);
    }
}
