using Domain.Entities;

namespace Infrastructure.Jwt
{
    /// <summary>
    /// Interface that defines the operation for generating a JWT token for a user.
    /// </summary>
    public interface IJwtGenerator
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user entity for whom the token will be generated.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        string GenerateToken(User user);
    }
}
