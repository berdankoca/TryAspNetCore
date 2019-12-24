using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TryAspNetCore.Core.Dependency;
using TryAspNetCore.Core.Uow;

namespace TryAspNetCore.EntityFrameworkCore.Uow
{
    public class UnitOfWork : IUnitOfWork, ITransientDependency
    {
        public Guid Id { get; } = Guid.NewGuid();

        public event EventHandler Disposed;

        public IUnitOfWork Outer { get; private set; }

        protected IDictionary<string, DbContext> ActiveDbContexts { get; }
        protected IDictionary<string, ActiveTransaction> ActiveTransactions { get; }

        public UnitOfWork()
        {
            ActiveDbContexts = new Dictionary<string, DbContext>();
        }

        public void Complete()
        {
            foreach (var activeContext in ActiveDbContexts)
            {
                activeContext.Value.SaveChanges();
            }

            foreach (var activeTransaction in ActiveTransactions)
            {
                activeTransaction.Value.DbTransaction.Commit();
                foreach (var dbContext in activeTransaction.Value.AttendedDbContexts)
                {
                    if (dbContext.Database.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager)
                        continue;

                    dbContext.Database.CommitTransaction();
                }
            }
        }

        public async Task CompleteAsync()
        {
            foreach (var activeContext in ActiveDbContexts)
            {
                await activeContext.Value.SaveChangesAsync();
            }

            
        }

        public TDbContext GetOrAddDbContext<TDbContext>(string key, Func<TDbContext> factory) where TDbContext : DbContext
        {
            if (!ActiveDbContexts.TryGetValue(key, out DbContext context))
            {
                context = factory();

                ActiveDbContexts.Add(key, context);
            }

            return (TDbContext)context;
        }

        public ActiveTransaction GetTransaction(string key)
        {
            ActiveTransactions.TryGetValue(key, out ActiveTransaction transaction);

            return transaction;
        }

        public void AddTransaction(string key, ActiveTransaction transaction)
        {
            if (!ActiveTransactions.ContainsKey(key))
            {
                ActiveTransactions.Add(key, transaction);
            }
        }

        public override string ToString()
        {
            return $"Uow-{ Id }";
        }

        public void Dispose()
        {
            Console.WriteLine($"Uow-{ Id } disposed!");

            Disposed.Invoke(this, new EventArgs());
        }

        public void SetOuter(IUnitOfWork outer)
        {
            Outer = outer;
        }
    }
}