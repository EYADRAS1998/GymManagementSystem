using Common.DTOs;

namespace GymSaga.API.Application.Services
{
    public interface IRegisterMemberSagaService
    {
        Task<Guid> ExecuteAsync(RegisterMemberDto dto, Guid userId, string token);

    }
}
