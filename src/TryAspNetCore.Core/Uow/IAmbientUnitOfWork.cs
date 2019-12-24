using System;

namespace TryAspNetCore.Core.Uow
{
    public interface IAmbientUnitOfWork
    {
        IUnitOfWork UnitOfWork { get; }

        void SetUnitOfWork(IUnitOfWork unitOfWork);
    }
}