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
            base.AttachToComponentRegistration(componentRegistry, registration);

            if (!registration.Activator.LimitType.ToString().StartsWith("TryAspNetCore"))
                return;

            // It will be good for the register interceptor
            // componentRegistry.Registered += (sender, e) =>
            // {
            //     if (!e.ComponentRegistration.Activator.LimitType.ToString().StartsWith("TryAspNetCore"))
            //         return;

            //     Console.WriteLine($"Registered: { e.ComponentRegistration.Activator.LimitType.ToString() }");
            // };

            // registration.Activating += (sender, e) =>
            // {
            //     if (!e.Instance.GetType().ToString().StartsWith("TryAspNetCore"))
            //         return;
            //     Console.WriteLine($"Activating: { e.Instance.GetType().ToString() }");
            // };

            // registration.Activated += (sender, e) =>
            // {
            //     Console.WriteLine("ACtivated");
            // };

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
                .SingleInstance()
                .PropertiesAutowired();
            //.OnRegistered
            //OnActivating

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => !t.IsGenericType)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => !t.IsGenericType)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();

            // base.Load(builder);
        }
    }
}