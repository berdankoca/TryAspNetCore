using System;
using Microsoft.EntityFrameworkCore;
using TryAspNetCore.Core.Uow;

namespace TryAspNetCore.EntityFrameworkCore.Uow
{
    public static class UnitOfWorkExtensions
    {
        public static TDbContext GetOrAddDbContext<TDbContext>(this IUnitOfWork unitOfWork, string name, Func<TDbContext> factory)
            where TDbContext : DbContext
        {
            return (unitOfWork as UnitOfWork).GetOrAddDbContext<TDbContext>(name, factory);
        }

        public static ActiveTransaction GetTransaction(this IUnitOfWork unitOfWork, string name)
        {
            return (unitOfWork as UnitOfWork).GetTransaction(name);
        }

        public static void AddTransaction(this IUnitOfWork unitOfWork, string name, ActiveTransaction transaction)
        {
            (unitOfWork as UnitOfWork).AddTransaction(name, transaction);
        }
    }
}