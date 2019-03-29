using System;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using TryAspNetCore.Core;
using TryAspNetCore.EntityFrameworkCore.Context;

namespace TryAspNetCore.EntityFrameworkCore.Repository
{
    public class WriteRepository<TContext, T> : ReadRepository<TContext, T>, IWriteRepository<TContext, T>
        where TContext : BaseContext
        where T : BaseEntity, new()
    {
        private bool _autoSave = true;
        public virtual bool AutoSave
        {
            get { return _autoSave; }
            set { _autoSave = value; }
        }

        private readonly TContext _context;

        public WriteRepository(TContext context)
            : base(context)
        {
            _context = context;
        }

        public virtual void Add(T entity)
        {
            Table.Add(entity);
            if (_autoSave)
            {
                Save();
            }
        }
        public async void AddAsync(T enttiy)
        {
            await Task.FromResult(Table.Add(enttiy));

            if (_autoSave)
            {
                await SaveAsync();
            }
        }

        public virtual void Delete(Guid id)
        {
            var entity = Get(id);
            if (entity == null)
                return;

            Delete(entity);
        }
        public async void DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
                return;

            DeleteAsync(entity);
        }
        public virtual void Delete(T entity)
        {
            Table.Remove(entity);
            if (_autoSave)
            {
                Save();
            }
        }
        public async void DeleteAsync(T entity)
        {
            await Task.FromResult(Table.Remove(entity));
            if (_autoSave)
            {
                await SaveAsync();
            }
        }

        public virtual void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            if (_autoSave)
            {
                Save();
            }
        }
        public async void UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            if (_autoSave)
            {
                await SaveAsync();
            }
        }

        public virtual int Save()
        {
            using (var tran = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var result = _context.SaveChanges();

                tran.Complete();

                return result;
            }
        }
        public async Task<int> SaveAsync()
        {
            using (var tran = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                var result = await _context.SaveChangesAsync();

                tran.Complete();

                return result;
            }
        }
    }
}