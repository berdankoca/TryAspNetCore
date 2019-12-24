namespace TryAspNetCore.Core
{
    public interface ISessionManager
    {
        DefaultSession Current { get; }
    }

}