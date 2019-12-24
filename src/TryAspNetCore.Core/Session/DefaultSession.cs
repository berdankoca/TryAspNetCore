using System;

namespace TryAspNetCore.Core
{
    public class DefaultSession
    {
        public const string ContextKey = "TryAspNetCore.DefaultSession";

        public Guid UserId { get; set; }

        public string UserEmail { get; set; }
    }
}