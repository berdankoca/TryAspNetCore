using System;
using System.Threading.Tasks;

namespace TryAspNetCore.Core.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }

        event EventHandler Disposed;

        IUnitOfWork Outer { get; }

        void Complete();

        Task CompleteAsync();

        void SetOuter(IUnitOfWork outer);
    }
}