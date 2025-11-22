using IdentityService.Application.DTOs;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Repositories;
using IdentityService.Infrastructure.Auth;
using System;
using System.Threading.Tasks;

namespace IdentityService.Application.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtToken;

        public AuthService(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtToken)
        {
            _unitOfWork = unitOfWork;
            _jwtToken = jwtToken;
        }

        public async Task<string> RegisterAsync(CreateUserDto dto)
        {
            // تحقق من وجود البريد أو اسم المستخدم
            if (await _unitOfWork.Users.ExistsByEmailAsync(dto.Email))
                throw new Exception("Email already exists");

            if (await _unitOfWork.Users.ExistsByUserNameAsync(dto.UserName))
                throw new Exception("UserName already exists");

            // 🔹 التحقق من الدور قبل إنشاء المستخدم
            var roleName = string.IsNullOrWhiteSpace(dto.Role) ? "User" : dto.Role;

            var role = await _unitOfWork.Roles.GetByNameAsync(roleName);
            if (role == null)
                throw new Exception($"Role '{roleName}' does not exist.");

            // إنشاء المستخدم
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            await _unitOfWork.Users.AddAsync(user);

            // إضافة الدور
            await _unitOfWork.UserRoles.AddUserRoleAsync(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            // حفظ
            await _unitOfWork.CommitAsync();

            return _jwtToken.GenerateToken(user);
        }


        public async Task<string> LoginAsync(string userNameOrEmail, string password)
        {
            var user = await _unitOfWork.Users.GetByUserNameAsync(userNameOrEmail)
                       ?? await _unitOfWork.Users.GetByEmailAsync(userNameOrEmail);

            if (user == null)
                throw new Exception("Invalid credentials");

            if (!VerifyPassword(password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return _jwtToken.GenerateToken(user);
        }

        public Task LogoutAsync(string userId)
        {
            // يمكن إدارة الجلسات أو RefreshTokens هنا
            return Task.CompletedTask;
        }

        public Task<string> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPassword(string password, string hashed)
        {
            var hashOfInput = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
            return hashOfInput == hashed;
        }
    }
}
