using System;
using System.Collections.Generic;

namespace TryAspNetCore.Core.Web
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException()
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }
}