using System;
using System.ComponentModel.DataAnnotations;
using TryAspNetCore.Api.Core;

namespace TryAspNetCore.Api.Domain
{
    public class EventRegistrationDto : BaseEntity
    {
        public override Guid Id { get; set; }

        public Guid UserId { get; set; }

    }
}