using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace TryAspNetCore.Test.Core
{
    public abstract class AppTestBase<TStartup> : TestBase
        where TStartup : class
    {
        protected override IServiceProvider ServiceProvider { get; }

        public AppTestBase()
        {
            var method = typeof(TStartup).GetMethod("ConfigureServices");
            if (method == null)
                throw new ArgumentNullException($"{ nameof(TStartup) } does not has a ConfigureServices method!");

            var services = new ServiceCollection();
            var startup = Activator.CreateInstance(typeof(TStartup));
            ServiceProvider = method.Invoke(startup, new object[] { services }) as IServiceProvider;            
        }
    }
}
