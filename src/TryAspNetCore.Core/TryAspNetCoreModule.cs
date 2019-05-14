using System;
using System.Linq;
using System.Reflection;
using Autofac;
using TryAspNetCore.Core.Dependency;

namespace TryAspNetCore.Core
{
    public class TryAspNetCoreModule : Autofac.Module
    {
        protected override void AttachToComponentRegistration(Autofac.Core.IComponentRegistry componentRegistry, Autofac.Core.IComponentRegistration registration)
        {
            // registration.Activating += (sender, e) =>
            // {
            //     Console.WriteLine("Activating");
            // };

            // registration.Activated += (sender, e) =>
            // {
            //     Console.WriteLine("ACtivated");
            // };

            //It will be good for the register interceptor
            //componentRegistry.Registered += (sender, e) =>
            //{
            //    Console.WriteLine("Registered");
            //};

            base.AttachToComponentRegistration(componentRegistry, registration);
        }
        protected override void Load(ContainerBuilder builder)
        {
            //Transient -> InstancePerDependency
            //Scoped    -> InstancePerLifetimeScope
            //Singleton -> SingleInstance

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => !t.IsGenericType)
                .AssignableTo<ISingletonDependency>()
                //Default behavior is implement itself(SessionManager), if we want to access via interface we add this line
                .AsImplementedInterfaces()
                .SingleInstance();
            //.OnRegistered
            //OnActivating

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => !t.IsGenericType)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => !t.IsGenericType)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}