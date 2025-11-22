using System.Threading.Tasks;
using IdentityService.Domain.Repositories;
using IdentityService.Infrastructure.Persistence;

namespace IdentityService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(
            ApplicationDbContext context,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserRoleRepository userRoleRepository)
        {
            _context = context;
            Users = userRepository;
            Roles = roleRepository;
            UserRoles = userRoleRepository;
        }

        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IUserRoleRepository UserRoles { get; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
