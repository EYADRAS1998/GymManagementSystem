using OrchestratorService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchestratorService.Application.Services
{
    public interface IMemberSubscriptionOrchestrator
    {
        Task<Guid> AddMemberWithSubscriptionAsync(AddMemberWithSubscriptionDto dto, string token);
        Task<Guid> RenewSubscriptionAsync(RenewSubscriptionDto dto, string token);
    }
}
