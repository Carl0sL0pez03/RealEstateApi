using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Jwt
{
    public class JwtGenerator(IConfiguration configuration) : IJwtGenerator
    {
        private readonly IConfiguration _config = configuration;

        public string GenerateToken(User user)
        {
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
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