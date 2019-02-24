using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TryAspNetCore.Api.Core
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