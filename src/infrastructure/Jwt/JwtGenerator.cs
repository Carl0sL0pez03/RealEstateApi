using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Jwt
{
    /// <summary>
    /// Class responsible for generating JWT tokens for users.
    /// Implements the <see cref="IJwtGenerator"/> interface.
    /// </summary>
    public class JwtGenerator(IConfiguration configuration) : IJwtGenerator
    {
        private readonly IConfiguration _config = configuration;

        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token will be generated.</param>
        /// <returns>A string representing the generated JWT token.</returns>
        public string GenerateToken(User user)
        {
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var keyString = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured.");

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Audience"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds
          );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}