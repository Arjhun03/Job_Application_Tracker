using JobApplicationTracker.Api.DTOs;

namespace JobApplicationTracker.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }
}