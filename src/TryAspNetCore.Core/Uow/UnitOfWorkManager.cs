using System;
using Microsoft.Extensions.DependencyInjection;
using TryAspNetCore.Core.Dependency;

namespace TryAspNetCore.Core.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager, ISingletonDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAmbientUnitOfWork _ambientUnitOfWork;

        public UnitOfWorkManager(IServiceProvider serviceProvider, IAmbientUnitOfWork ambientUnitOfWork)
        {
            _serviceProvider = serviceProvider;
            _ambientUnitOfWork = ambientUnitOfWork;
        }

        public IUnitOfWork Begin()
        {
            var uow = CreateUnitOfWork();

            return uow;
        }

        private IUnitOfWork CreateUnitOfWork()
        {
            var outerUow = _ambientUnitOfWork.UnitOfWork;

            var uow = _serviceProvider.GetRequiredService<IUnitOfWork>();
            uow.SetOuter(outerUow);
            uow.Disposed += (object sender, EventArgs e) =>
            {
                _ambientUnitOfWork.SetUnitOfWork(uow.Outer);
            };

            _ambientUnitOfWork.SetUnitOfWork(uow);

            return uow;
        }
    }
}