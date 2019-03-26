namespace TryAspNetCore.Api.Core
{
    public interface IAmbientDataContext
    {
        object GetData(string key);

        void SetData(string key, object value);
    }
}