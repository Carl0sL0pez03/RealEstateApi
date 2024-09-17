using Domain.Entities;

namespace Domain.Repositories
{
    /// <summary>
    /// Interface that defines operations for managing user data,
    /// including retrieving a user by their username and adding new users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The <see cref="User"/> object if found; otherwise, null.</returns>
        Task<User> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user object to be added to the system.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddUserAsync(User user);
    }
}
