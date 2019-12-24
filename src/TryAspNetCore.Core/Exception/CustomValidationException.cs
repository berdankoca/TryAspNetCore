using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TryAspNetCore.Core
{
    public class CustomValidationException : BaseException
    {
        public CustomValidationException()
        {
            ValidationErrors = new List<ValidationResult>();
        }

        public CustomValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationResult>();
        }

        public CustomValidationException(string message, ICollection<ValidationResult> validationResults)
            : base(message)
        {
            ValidationErrors = validationResults;
        }

        public ICollection<ValidationResult> ValidationErrors { get; set; }
    }
}