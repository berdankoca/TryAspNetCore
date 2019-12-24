using TryAspNetCore.Core.Dependency;
using TryAspNetCore.EntityFrameworkCore.Repository;

namespace TryAspNetCore.EventManagement
{
    public interface IEventRepository : IWriteRepository<EventContext, Event>, ITransientDependency
    {
        Event GetByEventCode(string eventCode);
    }
}