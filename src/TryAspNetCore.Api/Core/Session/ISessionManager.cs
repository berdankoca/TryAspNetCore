namespace TryAspNetCore.Api.Core
{
    public interface ISessionManager
    {
        DefaultSession Current { get; }
    }

}