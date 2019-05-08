using TryAspNetCore.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TryAspNetCore.EntityFrameworkCore.Context
{
    public abstract class BaseContext : DbContext
    {
        private readonly ISessionManager _sessionManager;

        public BaseContext(DbContextOptions options, ISessionManager sessionManager)
            : base(options)
        {
            _sessionManager = sessionManager;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var changedEntries = ChangeTracker.Entries<BaseEntity>().Where(r => r.State != EntityState.Deleted && r.State != EntityState.Unchanged);
            foreach (var item in changedEntries)
            {
                var entity = item.Entity as BaseEntity;
                if (item.State == EntityState.Added)
                    entity.SetCreatedInformation(_sessionManager);

                entity.SetUpdatedInfirmation(_sessionManager);
            }
            var result = base.SaveChanges();

            return result;
        }
    }
}