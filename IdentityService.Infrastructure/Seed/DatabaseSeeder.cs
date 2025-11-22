using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.Infrastructure.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedRolesAsync(context);
            await SeedAdminUserAsync(context);
        }

        private static async Task SeedRolesAsync(ApplicationDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var roles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), Name = "Admin" },
                new Role { Id = Guid.NewGuid(), Name = "User" }
            };

            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAdminUserAsync(ApplicationDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var admin = new User
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Email = "admin@gym.com",
                PasswordHash = HashPassword("Admin123!")
            };

            context.Users.Add(admin);
            await context.SaveChangesAsync();

            var adminRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");

            if (adminRole != null)
            {
                context.UserRoles.Add(new UserRole
                {
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                });

                await context.SaveChangesAsync();
            }
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
