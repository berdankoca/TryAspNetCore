using System;
using Microsoft.EntityFrameworkCore;

namespace TryAspNetCore.EntityFrameworkCore.Uow
{
    public interface IUnitOfWorkDbContextProvider<TDbContext> where TDbContext : DbContext
    {
        TDbContext GetDbContext();
    }
}