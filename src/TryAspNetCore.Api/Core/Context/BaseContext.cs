using System;
using TryAspNetCore.Api.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TryAspNetCore.Api.Core.Context
{
    public abstract class BaseContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var changedEntries = ChangeTracker.Entries<BaseEntity>().Where(r => r.State != EntityState.Deleted && r.State != EntityState.Unchanged);
            foreach (var item in changedEntries)
            {
                var entity = item.Entity as BaseEntity;
                //
                var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (item.State == EntityState.Added)
                {
                    entity.CreatedBy = userId;
                    entity.CreatedDate = DateTime.Now;
                }
                entity.UpdatedBy = userId;
                entity.UpdatedDate = DateTime.Now;
            }
            var result = base.SaveChanges();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b =>
                {
                    b.ToTable("User");
                });

            builder.Entity<UserClaim>(b =>
            {
                b.ToTable("UserClaim");
            });

            builder.Entity<UserLogin>(b =>
            {
                b.ToTable("UserLogin");
            });

            builder.Entity<UserToken>(b =>
            {
                b.ToTable("UserToken");
            });

            builder.Entity<Role>(b =>
            {
                b.ToTable("Role");
            });

            builder.Entity<RoleClaim>(b =>
            {
                b.ToTable("RoleClaim");
            });

            builder.Entity<UserRole>(b =>
            {
                b.ToTable("UserRole");
            });
        }
    }
}