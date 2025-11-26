using Common;
using SubscriptionService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<Subscription> GetByIdAsync(Guid id);
        Task<PagedResult<Subscription>> GetAllAsync(int pageNumber, int pageSize);
        Task<PagedResult<Subscription>> GetByMemberIdAsync(Guid memberId, int pageNumber, int pageSize);
        Task AddAsync(Subscription subscription);
        void Update(Subscription subscription);
        void Delete(Subscription subscription);
    }
}
