using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionService.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ISubscriptionRepository Subscriptions { get; }
        IPlanRepository Plans { get; }

        Task<int> SaveChangesAsync();
    }
}
