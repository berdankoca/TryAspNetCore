using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TryAspNetCore.Core;

namespace TryAspNetCore.EventManagement
{
    public class EventDto : BaseEntity
    {
        public EventDto()
        {
            // Registrations = new List<EventRegistrationDto>();
        }
        public override Guid Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Title { get; set; }

        public IEnumerable<EventRegistrationDto> Registrations { get; set; }

    }
}