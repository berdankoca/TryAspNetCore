using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using TryAspNetCore.Core.Dependency;
using TryAspNetCore.Core.Uow;

namespace TryAspNetCore.EntityFrameworkCore.Uow
{
    public class UnitOfWorkDbContextProvider<TDbContext> : IUnitOfWorkDbContextProvider<TDbContext>, ITransientDependency
        where TDbContext : DbContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAmbientUnitOfWork _ambientUnitOfWork;

        public UnitOfWorkDbContextProvider(IServiceProvider serviceProvider, IAmbientUnitOfWork ambientUnitOfWork)
        {
            _serviceProvider = serviceProvider;
            _ambientUnitOfWork = ambientUnitOfWork;
        }
        public TDbContext GetDbContext()
        {
            string key = typeof(TDbContext).FullName;
            var context = _ambientUnitOfWork.UnitOfWork.GetOrAddDbContext(key, () => CreateDbContext());
            return context;
        }

        private TDbContext CreateDbContext()
        {
            var transactionKey = $"EFTransaction_{ typeof(TDbContext).FullName }";
            var context = _serviceProvider.GetRequiredService<TDbContext>();
            var activeTransaction = _ambientUnitOfWork.UnitOfWork.GetTransaction(transactionKey);
            if (activeTransaction == null)
            {
                var dbTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                _ambientUnitOfWork.UnitOfWork.AddTransaction(transactionKey, new ActiveTransaction(dbTransaction));
            }
            else
            {
                if (context.Database.GetService<IDbContextTransactionManager>() is IRelationalTransactionManager)
                {
                    context.Database.UseTransaction(activeTransaction.DbTransaction.GetDbTransaction());
                }
                else
                {
                    context.Database.BeginTransaction();
                }

                activeTransaction.AttendedDbContexts.Add(context);
            }

            return context;
        }
    }
}