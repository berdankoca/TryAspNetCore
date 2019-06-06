using TryAspNetCore.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            try
            {
                var validationResults = new List<ValidationResult>();
                var changedEntries = ChangeTracker.Entries<BaseEntity>().Where(r => r.State != EntityState.Deleted && r.State != EntityState.Unchanged);
                foreach (var entry in changedEntries)
                {
                    var entity = entry.Entity as BaseEntity;
                    if (entry.State == EntityState.Added)
                        entity.SetCreatedInformation(_sessionManager);

                    entity.SetUpdatedInfirmation(_sessionManager);

                    validationResults.AddRange(entity.Validate());
                }

                if (validationResults.Any())
                    throw new CustomValidationException("Model properties is not valid!", validationResults);

                var result = base.SaveChanges();

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Handle with user friendly exception
                throw ex;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                ChangeTracker.DetectChanges();
                var changedEntries = ChangeTracker.Entries<BaseEntity>().Where(r => r.State != EntityState.Deleted && r.State != EntityState.Unchanged);
                foreach (var entry in changedEntries)
                {
                    var entity = entry.Entity as BaseEntity;
                    if (entry.State == EntityState.Added)
                        entity.SetCreatedInformation(_sessionManager);

                    entity.SetUpdatedInfirmation(_sessionManager);

                    entity.Validate();
                }
                var result = await base.SaveChangesAsync();

                return result;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Handle with user friendly exception
                throw ex;
            }

        }
    }
}