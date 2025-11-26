using SubscriptionService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Domain.Repositories
{
    public interface IPlanRepository
    {
        Task<Plan> GetByIdAsync(Guid id);
        Task<IEnumerable<Plan>> GetAllAsync();
        Task AddAsync(Plan plan);
        void Update(Plan plan);
        void Delete(Plan plan);
    }
}
