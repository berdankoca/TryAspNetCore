using System;

namespace TryAspNetCore.Api.Core
{
    public class DefaultSession
    {
        public const string ContextKey = "TryAspNetCore.";

        public Guid UserId { get; set; }

        public string UserEmail { get; set; }
    }
}