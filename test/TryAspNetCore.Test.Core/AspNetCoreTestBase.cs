using System;
using System.Collections.Generic;
using System.Text;

namespace TryAspNetCore.Test.Core
{
    public abstract class AspNetCoreTestBase<TStartup> : TestBase
        where TStartup : class
    {
    }
}
