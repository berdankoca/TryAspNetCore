using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Core.Context;

namespace TryAspNetCore.Api.Core.Repositories
{
    public interface IWriteRepository<TContext, T> : IReadRepository<TContext, T>
        where TContext : BaseContext
        where T : BaseEntity, new()
    {
        void Add(T enttiy);

        void Update(T entity);

        void Delete(T entity);

        void Save();

    }
}