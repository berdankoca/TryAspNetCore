using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Core.Context;

namespace TryAspNetCore.Api.Core.Repositories
{
    public class ReadRepository<TContext, T> : IReadRepository<TContext, T>
        where TContext : BaseContext
        where T : BaseEntity, new()
    {
        private readonly TContext _context;
        public DbSet<T> Table => _context.Set<T>();

        public ReadRepository(TContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression);
        }

        public virtual T Get(Guid id)
        {
            return FindBy(e => e.Id == id).SingleOrDefault();
        }

        public virtual T GetBy(Expression<Func<T, bool>> expression)
        {
            return FindBy(expression).FirstOrDefault();
        }
    }
}