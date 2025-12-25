using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PaymentService.Domian.Entities;

namespace PaymentService.Infrastructure.Persistence
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<PaymentInstallment> PaymentInstallments => Set<PaymentInstallment>();
        public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.TotalAmount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Currency)
                      .HasMaxLength(10)
                      .IsRequired();
            });

            modelBuilder.Entity<PaymentInstallment>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Amount)
                      .HasColumnType("decimal(18,2)");
            });
        }
    }
}
