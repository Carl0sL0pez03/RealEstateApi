using Domain.Entities;

namespace Infrastructure.Jwt
{
    public interface IJwtGenerator
    {
        string GenerateToken(User user);
    }
}