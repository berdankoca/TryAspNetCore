using TryAspNetCore.EntityFrameworkCore.Repository;
using TryAspNetCore.EntityFrameworkCore.Uow;

namespace TryAspNetCore.EventManagement
{
    public class EventRepository : WriteRepository<EventContext, Event>, IEventRepository
    {
        public EventRepository(IUnitOfWorkDbContextProvider<EventContext> unitOfWorkDbContextProvider)
            : base(unitOfWorkDbContextProvider)
        {
        }

        public Event GetByEventCode(string eventCode)
        {
            throw new System.NotImplementedException();
        }
    }
}