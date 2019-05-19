using System;
using TryAspNetCore.Core;

namespace TryAspNetCore.EntityFrameworkCore.Tests
{
    public class Dummy : BaseEntity
    {
        public override Guid Id { get; set; }

        public string Name { get; set; }
    }
}