using System;
using System.ComponentModel.DataAnnotations;
using TryAspNetCore.Core;

namespace TryAspNetCore.EntityFrameworkCore.Tests
{
    public class Dummy : BaseEntity
    {
        public override Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}