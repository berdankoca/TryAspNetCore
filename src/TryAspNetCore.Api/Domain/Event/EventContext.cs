using System;
using TryAspNetCore.Api.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TryAspNetCore.Api.Core.Context
{
    public class EventContext : BaseContext
    {
        public EventContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options, httpContextAccessor)
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