using IdentityService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserAsync(Guid id);
        Task<List<UserDto>> GetAllUsersAsync();
        Task CreateUserAsync(CreateUserDto dto);
    }
}
