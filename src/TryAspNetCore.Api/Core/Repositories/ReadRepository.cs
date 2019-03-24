using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
        public async Task<List<T>> FindByAsync(Expression<Func<T, bool>> expression)
        {
            return await FindBy(expression).ToListAsync();
        }

        public virtual T Get(Guid id)
        {
            return FindBy(e => e.Id == id).SingleOrDefault();
        }
        public async Task<T> GetAsync(Guid id)
        {
            return await FindBy(e => e.Id == id).SingleOrDefaultAsync();
        }

        public virtual T GetBy(Expression<Func<T, bool>> expression)
        {
            return FindBy(expression).FirstOrDefault();
        }
        public async Task<T> GetByAsync(Expression<Func<T, bool>> expression)
        {
            return await FindBy(expression).FirstOrDefaultAsync();
        }
    }
}