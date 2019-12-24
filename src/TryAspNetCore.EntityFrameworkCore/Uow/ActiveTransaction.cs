using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;

namespace TryAspNetCore.EntityFrameworkCore.Uow
{
    public class ActiveTransaction
    {
        public Guid Id => Guid.NewGuid();

        public IDbContextTransaction DbTransaction { get;}

        public List<DbContext> AttendedDbContexts { get; }

        public ActiveTransaction(IDbContextTransaction dbTransaction)
        {
            DbTransaction = dbTransaction;
            AttendedDbContexts = new List<DbContext>();
        }

        public override string ToString()
        {
            return $"Transaction-{ Id }";
        }
    }
}
