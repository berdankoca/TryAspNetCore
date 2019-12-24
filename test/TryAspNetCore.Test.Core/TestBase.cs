using System;
using System.Collections.Generic;
using System.Text;

namespace TryAspNetCore.Test.Core
{
    public abstract class TestBase
    {
        protected abstract IServiceProvider ServiceProvider { get; }
    }
}
