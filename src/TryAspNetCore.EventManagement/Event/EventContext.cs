using TryAspNetCore.Core;
using Microsoft.EntityFrameworkCore;
using TryAspNetCore.EntityFrameworkCore.Context;

namespace TryAspNetCore.EventManagement
{
    public class EventContext : BaseContext
    {
        public EventContext(DbContextOptions options, ISessionManager sessionManager)
            : base(options, sessionManager)
        {

        }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.Entity<Event>()
            // .HasMany(er => er.Registrations)
            // .WithOne()
            // .HasForeignKey(er => er.EventId);

            base.OnModelCreating(builder);
        }

    }
}