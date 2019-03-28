using System;
using TryAspNetCore.Api.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace TryAspNetCore.Api.Core.Context
{
    public class IdentityContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {

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