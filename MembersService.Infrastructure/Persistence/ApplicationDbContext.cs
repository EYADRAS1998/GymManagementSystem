using MembersService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace MembersService.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberProgress> MemberProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // Member
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.FullName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(m => m.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(m => m.Email)
                      .HasMaxLength(100);

                entity.Property(m => m.Gender)
                      .HasMaxLength(10);

                entity.Property(m => m.Notes)
                      .HasMaxLength(1000);

                entity.Property(m => m.CreatedAt)
                      .IsRequired();

                entity.HasMany(m => m.ProgressRecords)
                      .WithOne(p => p.Member)
                      .HasForeignKey(p => p.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // -----------------------------
            // MemberProgress
            modelBuilder.Entity<MemberProgress>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Weight)
                      .IsRequired()
                      .HasColumnType("decimal(5,2)");

                entity.Property(p => p.MeasurementsJson)
                      .HasColumnType("nvarchar(max)");

                entity.Property(p => p.Notes)
                      .HasMaxLength(1000);

                entity.Property(p => p.RecordedAt)
                      .IsRequired();
            });
        }
    }
}
