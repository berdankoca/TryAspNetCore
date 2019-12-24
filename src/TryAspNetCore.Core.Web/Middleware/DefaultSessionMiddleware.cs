using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TryAspNetCore.Core.Uow;

namespace TryAspNetCore.Core.Web
{
    public class DefaultSessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAmbientDataContext _ambient;

        public DefaultSessionMiddleware(RequestDelegate next, IUnitOfWorkManager unitOfWorkManager, IAmbientDataContext ambient)
        {
            _next = next;
            _unitOfWorkManager = unitOfWorkManager;
            _ambient = ambient;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                if (httpContext.User.Identity.IsAuthenticated)
                {
                    var session = new DefaultSession
                    {
                        UserId = Guid.Parse(httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                    };
                    _ambient.SetData(DefaultSession.ContextKey, session);
                }
                await _next(httpContext);

                await uow.CompleteAsync();
            }
        }


    }
}