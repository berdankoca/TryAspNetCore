using System.Collections.Concurrent;
using System.Threading;
using TryAspNetCore.Core.Dependency;

namespace TryAspNetCore.Core
{
    //https://codereview.stackexchange.com/questions/140694/ambient-context
    //This implementation is not necessary, I just the want to understand and try it
    public class AmbientDataContext : IAmbientDataContext, ISingletonDependency
    {
        private static readonly ConcurrentDictionary<string, AsyncLocal<object>> Instance = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public object GetData(string key)
        {
            var localData = Instance.GetOrAdd(key, (k) => new AsyncLocal<object>());
            return localData.Value;
        }

        public void SetData(string key, object value)
        {
            var localData = Instance.GetOrAdd(key, (k) => new AsyncLocal<object>());
            localData.Value = value;
        }
    }
}