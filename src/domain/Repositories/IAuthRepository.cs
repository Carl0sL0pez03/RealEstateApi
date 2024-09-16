using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<string> AuthenticateAsync(string username, string password);
        Task RegisterAsync(User user);
    }
}