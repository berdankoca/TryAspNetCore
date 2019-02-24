using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TryAspNetCore.Api.Core;

namespace TryAspNetCore.Api.Domain
{
    [Table("Event")]
    public class Event : BaseEntity
    {
        public Event()
        {
            Registrations = new Collection<EventRegistration>();
        }

        [Column("EventId")]
        public override Guid Id { get; set; }

        public string Title { get; set; }

        [ForeignKey("EventId")]
        public ICollection<EventRegistration> Registrations { get; set; }
    }
}
