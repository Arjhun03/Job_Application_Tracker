using System.Security.Cryptography;
using System.Text;
using JobApplicationTracker.Api.DTOs;
using JobApplicationTracker.Api.Models;
using JobApplicationTracker.Api.Repositories.Interfaces;
using JobApplicationTracker.Api.Services.Interfaces;

namespace JobApplicationTracker.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email address already exists.");
            }

            // Securely hash password using HMACSHA512
            using var hmac = new HMACSHA512();
            var passwordHashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            var passwordHashString = Convert.ToBase64String(passwordHashBytes);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHashString,
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(user);

            return new RegisterResponse
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}