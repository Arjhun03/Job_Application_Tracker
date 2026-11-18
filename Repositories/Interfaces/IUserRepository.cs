using JobApplicationTracker.Api.Models;

namespace JobApplicationTracker.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}