using System;
using System.Linq;
using System.Linq.Expressions;
using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Core.Context;

namespace TryAspNetCore.Api.Core.Repositories
{
    public interface IReadRepository<TContext, T>
        where TContext : BaseContext
        where T : BaseEntity, new()
    {
        T Get(Guid id);

        T GetBy(Expression<Func<T, bool>> expression);

        IQueryable<T> FindBy(Expression<Func<T, bool>> expression);
    }
}