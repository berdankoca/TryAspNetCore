using System;
using System.Threading.Tasks;
using TryAspNetCore.Core;
using TryAspNetCore.EntityFrameworkCore.Context;

namespace TryAspNetCore.EntityFrameworkCore.Repository
{
    public interface IWriteRepository<TContext, T> : IReadRepository<TContext, T>
        where TContext : BaseContext
        where T : BaseEntity, new()
    {
        void Add(T enttiy);
        void AddAsync(T enttiy);

        void Update(T entity);
        void UpdateAsync(T entity);

        void Delete(Guid id);
        void DeleteAsync(Guid id);
        void Delete(T entity);
        void DeleteAsync(T entity);

        int Save();
        Task<int> SaveAsync();

    }
}