using SubscriptionService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Application.Services
{
    public interface IPlanService
    {
        Task<PlanReadDto> GetByIdAsync(Guid id);
        Task<IEnumerable<PlanReadDto>> GetAllAsync();
        Task<PlanReadDto> CreateAsync(PlanCreateDto dto);
        Task<PlanReadDto> UpdateAsync(Guid id, PlanUpdateDto dto);
        Task DeleteAsync(Guid id);
    }
}
