using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Repositories;

namespace IdentityService.Application.Services.Impl
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateRoleAsync(string roleName)
        {
            // تحقق من وجود الدور مسبقاً
            if (await _unitOfWork.Roles.ExistsAsync(roleName))
                throw new Exception("Role already exists");

            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = roleName
            };

            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<string>> GetAllRolesAsync()
        {
            var roles = await _unitOfWork.Roles.GetAllAsync();
            return roles.Select(r => r.Name).ToList();
        }
    }
}
