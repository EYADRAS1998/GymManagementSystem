using IdentityService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Domain.Repositories
{
    public interface IUserRoleRepository
    {
        Task<List<Role>> GetRolesForUserAsync(Guid userId);
        Task<List<User>> GetUsersInRoleAsync(Guid roleId);
        Task AddUserRoleAsync(UserRole userRole);
        Task RemoveUserRoleAsync(UserRole userRole);
        Task<bool> UserHasRoleAsync(Guid userId, string roleName);
    }
}
