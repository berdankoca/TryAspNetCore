using System.Transactions;
using Microsoft.EntityFrameworkCore;
using TryAspNetCore.Api.Core;
using TryAspNetCore.Api.Core.Context;

namespace TryAspNetCore.Api.Core.Repositories
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

        public virtual void Delete(T entity)
        {
            Table.Remove(entity);
            if (_autoSave)
            {
                Save();
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

        public virtual void Save()
        {
            using (var tran = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                _context.SaveChanges();

                tran.Complete();
            }
        }
    }
}