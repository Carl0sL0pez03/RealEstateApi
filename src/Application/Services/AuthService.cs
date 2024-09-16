using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Jwt;

namespace Application.Services
{
    public class AuthService(IUserRepository userRepository, IJwtGenerator jwtGenerator) : IAuthRepository
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtGenerator _jwtGenerator = jwtGenerator;

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials");
            }

            return _jwtGenerator.GenerateToken(user);
        }

        public async Task RegisterAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _userRepository.AddUserAsync(user);
        }


        private static bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}