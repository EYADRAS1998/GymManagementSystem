using IdentityService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Services
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string userNameOrEmail, string password);
        Task<string> RegisterAsync(CreateUserDto dto);  // دالة إنشاء حساب
        Task LogoutAsync(string userId);                  // اختياري
        Task<string> RefreshTokenAsync(string refreshToken); // اختياري
    }
}
