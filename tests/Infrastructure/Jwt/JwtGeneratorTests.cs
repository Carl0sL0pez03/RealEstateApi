using NUnit.Framework;
using Microsoft.Extensions.Caching.Memory;
using Moq;

using Domain.Entities;
using Infrastructure.Jwt;

namespace RealEstateApi.tests.Infrastructure.Jwt
{
    /// <summary>
    /// Unit tests for the JwtGenerator class, testing the token generation process with caching.
    /// </summary>
    [TestFixture]
    public class JwtGeneratorTests
    {
        private JwtGenerator _jwtGenerator = null!;
        private Mock<IConfiguration> _mockConfig = null!;
        private Mock<IMemoryCache> _mockCache = null!;
        private User _testUser = null!;

        /// <summary>
        /// Setup method that is called before each test.
        /// It initializes the mocked configuration, memory cache, and the JwtGenerator instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _mockConfig = new Mock<IConfiguration>();

            _mockConfig.Setup(c => c["Jwt:Key"]).Returns("SuperSecretKey012345678901234567890");
            _mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("testIssuer");
            _mockConfig.Setup(c => c["Jwt:Audience"]).Returns("testAudience");

            _mockCache = new Mock<IMemoryCache>();

            _testUser = new User { Username = "test2user", PasswordHash = "123232" };

            _jwtGenerator = new JwtGenerator(_mockConfig.Object, _mockCache.Object);
        }

       /*  /// <summary>
        /// Test to verify that if a token already exists in the cache, it is returned rather than generating a new one.
        /// </summary>
        [Test]
        public void GenerateToken_ShouldReturnCachedToken_IfTokenExistsInCache()
        {
            var cachedToken = "cachedJwtToken";
            _mockCache.Setup(c => c.TryGetValue(It.IsAny<object>(), out cachedToken))
              .Returns(true);


            var result = _jwtGenerator.GenerateToken(_testUser);
            Assert.AreEqual(cachedToken, result);
        }
 */
        /// <summary>
        /// Test to verify that if no token exists in the cache, a new token is generated.
        /// </summary>
        [Test]
        public void GenerateToken_ShouldGenerateNewToken_IfNotInCache()
        {
            object? tokenFromCache = null;
            _mockCache.Setup(c => c.TryGetValue(It.IsAny<object>(), out tokenFromCache)).Returns(false);

            var cacheEntryMock = new Mock<ICacheEntry>();
            _mockCache.Setup(c => c.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

            var result = _jwtGenerator.GenerateToken(_testUser);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<string>(result);
        }
    }
}