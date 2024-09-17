using Domain.Entities;
using Infrastructure.Data.Jwt;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace RealEstateApi.tests.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Test class for the UserRepository to ensure it correctly handles user-related operations,
    /// such as adding users and retrieving users by username.
    /// </summary>
    [TestFixture]
    public class UserRepositoryTests
    {
        /// <summary>
        /// Test to verify that AddUserAsync successfully adds a new user to the database.
        /// </summary>
        [Test]
        public async Task AddUserAsync_ShouldAddUserToDbContext()
        {
            var options = new DbContextOptionsBuilder<RealEstateDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_AddUser")
                .Options;

            using var context = new RealEstateDbContext(options);

            var repository = new UserRepository(context);
            var newUser = new User
            {
                Username = "testuser",
                PasswordHash = "hashedpassword123"
            };

            await repository.AddUserAsync(newUser);

            var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Username == "testuser");
            Assert.IsNotNull(savedUser);
            Assert.AreEqual("testuser", savedUser.Username);
        }
    }
}