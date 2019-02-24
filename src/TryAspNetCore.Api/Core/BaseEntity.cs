using System;

namespace TryAspNetCore.Api.Core
{
    public abstract class BaseEntity
    {
        public abstract Guid Id { get; set; }
    }
}