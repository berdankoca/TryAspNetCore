using TryAspNetCore.EntityFrameworkCore.Repository;

namespace TryAspNetCore.EventManagement
{
    public class EventRepository : WriteRepository<EventContext, Event>, IEventRepository
    {
        public EventRepository(EventContext context)
            : base(context)
        {
        }

        public Event GetByEventCode(string eventCode)
        {
            throw new System.NotImplementedException();
        }
    }
}