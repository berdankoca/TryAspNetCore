using System;
using System.ComponentModel.DataAnnotations;
using TryAspNetCore.Core;

namespace TryAspNetCore.EventManagement
{
    public class EventRegistrationDto : BaseEntity
    {
        public override Guid Id { get; set; }

        public Guid UserId { get; set; }

    }
}