using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TryAspNetCore.Core;
using TryAspNetCore.EntityFrameworkCore.Context;

namespace TryAspNetCore.EntityFrameworkCore.Repository
{
    public interface IReadRepository<TContext, T>
        where TContext : BaseContext
        where T : BaseEntity, new()
    {
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);

        T GetBy(Expression<Func<T, bool>> expression);
        Task<T> GetByAsync(Expression<Func<T, bool>> expression);

        IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> expression);
    }
}