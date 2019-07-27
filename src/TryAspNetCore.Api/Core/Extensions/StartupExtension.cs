using System;
using System.Reflection;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TryAspNetCore.EntityFrameworkCore.Context;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using TryAspNetCore.Core.Web;
using TryAspNetCore.EntityFrameworkCore.Repository;
using TryAspNetCore.Core;
using TryAspNetCore.Core.Dependency;
using Autofac.Core;
using System.Diagnostics;

namespace TryAspNetCore.Api.Core
{
    public static class StartupExtension
    {
        public static IServiceProvider RegisterAutofacImplementation(this IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            //Register all needed dependency
            // builder.RegisterType<JwtFactory>()
            //     .As<IJwtFactory>()
            //     .SingleInstance();

            builder.RegisterModule(new TryAspNetCoreModule());

            builder.RegisterGeneric(typeof(ReadRepository<,>))
                .As(typeof(IReadRepository<,>))
                .InstancePerDependency()
                .PropertiesAutowired();

            builder.RegisterGeneric(typeof(WriteRepository<,>))
                .As(typeof(IWriteRepository<,>))
                .InstancePerDependency()
                .PropertiesAutowired();

            // services.AddSingleton<IJwtFactory, JwtFactory>();
            // services.AddTransient(typeof(IReadRepository<,>), typeof(ReadRepository<,>));
            // services.AddTransient(typeof(IWriteRepository<,>), typeof(WriteRepository<,>));

            // services.ScanAssembliesAndRegister();
            // services.AddSingleton<IAmbientDataContext, AmbientDataContext>();
            // services.AddSingleton<ISessionManager, SessionManager>();

            IContainer container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        public static IServiceCollection AddJWtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtIssuer"],
                    ValidAudience = configuration["JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static IServiceCollection ScanAssembliesAndRegister(this IServiceCollection services)
        {
            // Assembly.GetEntryAssembly().GetReferencedAssemblies().Select(Assembly.Load)
            var typesFromAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.DefinedTypes.Where(d => d.GetInterfaces()
                    .Any(di => (di == typeof(ISingletonDependency) || di == typeof(ITransientDependency) || di == typeof(IScopedDependency)))));
            foreach (var type in typesFromAssembly)
            {
                var interfaces = type.GetInterfaces();
                var interfaceType = interfaces.First(i => i.Name == $"I{ type.Name }");
                var dependencyInterfaceType = interfaces.First(i => i.Name.EndsWith("Dependency"));
                services.Add(new ServiceDescriptor(interfaceType, type, GetImplementationServiceLifeTime(dependencyInterfaceType)));
            }

            return services;
        }

        // public static IServiceCollection ScanAssembliesAndRegisterDBContext(this IServiceCollection services, IConfiguration configuration)
        // {
        //     var typesFromAssembly = AppDomain.CurrentDomain.GetAssemblies()
        //         .SelectMany(a => a.DefinedTypes.Where(d => !d.IsAbstract && d.IsSubclassOf(typeof(BaseContext))));

        //     foreach (var type in typesFromAssembly)
        //     {
        //         // services.AddDbContext()
        //     }

        //     return services;
        // }

        private static ServiceLifetime GetImplementationServiceLifeTime(Type dependencyInterfaceType)
        {
            if (dependencyInterfaceType == typeof(ISingletonDependency))
                return ServiceLifetime.Singleton;
            else if (dependencyInterfaceType == typeof(ITransientDependency))
                return ServiceLifetime.Transient;
            else if (dependencyInterfaceType == typeof(IScopedDependency))
                return ServiceLifetime.Scoped;
            else
                throw new Exception($"Undefined dependency type! (Type: { nameof(dependencyInterfaceType) })");
        }
    }
}