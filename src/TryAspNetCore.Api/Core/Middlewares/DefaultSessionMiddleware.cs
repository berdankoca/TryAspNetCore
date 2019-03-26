using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TryAspNetCore.Api.Core
{
    public class DefaultSessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAmbientDataContext _ambient;

        public DefaultSessionMiddleware(RequestDelegate next, IAmbientDataContext ambient)
        {
            _next = next;
            _ambient = ambient;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var session = new DefaultSession
            {
                UserId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
            _ambient.SetData(DefaultSession.ContextKey, session);
            await _next(httpContext);
        }


    }
}