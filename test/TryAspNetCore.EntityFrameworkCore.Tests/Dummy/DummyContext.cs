using Microsoft.EntityFrameworkCore;
using TryAspNetCore.Core;
using TryAspNetCore.EntityFrameworkCore.Context;

namespace TryAspNetCore.EntityFrameworkCore.Tests
{
    public class DummyContext : BaseContext
    {
        public DummyContext(DbContextOptions options, ISessionManager sessionManager)
            : base(options, sessionManager)
        {

        }

        public DbSet<Dummy> Dummies { get; set; }
    }
}