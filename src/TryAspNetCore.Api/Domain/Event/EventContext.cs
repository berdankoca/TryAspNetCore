using System;
using TryAspNetCore.Api.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TryAspNetCore.Api.Core.Context
{
    public class EventContext : BaseContext
    {
        public EventContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }

    }
}