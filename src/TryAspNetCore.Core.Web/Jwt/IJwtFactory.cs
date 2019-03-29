using System;

namespace TryAspNetCore.Core.Web
{
    public interface IJwtFactory
    {
        string GenerateToken(Guid userId);
    }
}