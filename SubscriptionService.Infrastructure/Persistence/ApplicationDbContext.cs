using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Plan> Plans { get; set; }
    }
}
