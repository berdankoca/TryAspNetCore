using System;

namespace TryAspNetCore.Core.Uow
{
    public interface IUnitOfWorkManager
    {
        IUnitOfWork Begin();
    }
}