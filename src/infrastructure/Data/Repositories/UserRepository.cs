using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Jwt
{
    /// <summary>
    /// Repository that implements user-related operations, 
    /// such as retrieving a user by username and adding new users.
    /// </summary>
    public class UserRepository(RealEstateDbContext dbContext) : IUserRepository
    {
        private readonly RealEstateDbContext _dbContext = dbContext;

        /// <summary>
        /// Retrieves a user from the database by their username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The <see cref="User"/> entity if found; otherwise, null.</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username) ?? throw new Exception("User not found");
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">The user entity to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddUserAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}