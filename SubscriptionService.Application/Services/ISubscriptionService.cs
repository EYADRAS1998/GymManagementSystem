using SubscriptionService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Services
{
    public interface ISubscriptionService
    {
        Task<SubscriptionReadDto> GetByIdAsync(Guid id);
        Task<SubscriptionPagedDto> GetAllAsync(int pageNumber, int pageSize);
        Task<SubscriptionPagedDto> GetByMemberIdAsync(Guid memberId, int pageNumber, int pageSize);
        Task<SubscriptionReadDto> CreateAsync(SubscriptionCreateDto dto, Guid createdBy);
        Task<SubscriptionReadDto> UpdateAsync(Guid id, SubscriptionUpdateDto dto);
        Task DeleteAsync(Guid id);
        Task CancelAsync(Guid subscriptionId);

    }
}
