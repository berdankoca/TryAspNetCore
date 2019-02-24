using TryAspNetCore.Api.Domain;

namespace TryAspNetCore.Api.Core
{
    public interface IJwtFactory
    {
        string GenerateToken(User user);
    }
}