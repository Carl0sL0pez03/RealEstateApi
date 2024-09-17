using Domain.Entities;

namespace Domain.Repositories
{
    /// <summary>
    /// Interface that defines authentication and registration operations for users.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Authenticates a user by validating their username and password.
        /// </summary>
        /// <param name="username">The username of the user attempting to authenticate.</param>
        /// <param name="password">The password of the user attempting to authenticate.</param>
        /// <returns>A JWT token if the authentication is successful.</returns>
        Task<string> AuthenticateAsync(string username, string password);

        /// <summary>
        /// Registers a new user by saving the user's details in the system.
        /// </summary>
        /// <param name="user">The user object containing the username and password hash.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task RegisterAsync(User user);
    }
}
