namespace TryAspNetCore.Api.Core
{
    public class SessionManager : ISessionManager
    {
        private readonly IAmbientDataContext _ambient;

        public SessionManager(IAmbientDataContext ambient)
        {
            _ambient = ambient;
        }
        public DefaultSession Current
        {
            get
            {
                return (DefaultSession)_ambient.GetData(DefaultSession.ContextKey);
            }
        }
    }
}