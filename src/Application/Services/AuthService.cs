using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Jwt;

namespace Application.Services
{
    /// <summary>
    /// Service responsible for user authentication and registration.
    /// Handles user verification and token generation using JWT.
    /// </summary>
    public class AuthService(IUserRepository userRepository, IJwtGenerator jwtGenerator) : IAuthRepository
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtGenerator _jwtGenerator = jwtGenerator;

        /// <summary>
        /// Authenticates a user by validating their username and password.
        /// If valid, a JWT token is generated and returned.
        /// </summary>
        /// <param name="username">The username of the user attempting to authenticate.</param>
        /// <param name="password">The password of the user attempting to authenticate.</param>
        /// <returns>A JWT token if the authentication is successful.</returns>
        /// <exception cref="Exception">Thrown if the credentials are invalid.</exception>
        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Invalid credentials");
            }

            return _jwtGenerator.GenerateToken(user);
        }

        /// <summary>
        /// Registers a new user by hashing their password and storing them in the user repository.
        /// </summary>
        /// <param name="user">The user to be registered, with a username and plain-text password.</param>
        public async Task RegisterAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _userRepository.AddUserAsync(user);
        }

        /// <summary>
        /// Verifies if the provided password matches the stored hashed password.
        /// </summary>
        /// <param name="password">The plain-text password provided by the user.</param>
        /// <param name="storedHash">The hashed password stored in the database.</param>
        /// <returns>True if the password matches; otherwise, false.</returns>
        private static bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}