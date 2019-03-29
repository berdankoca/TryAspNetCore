using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using TryAspNetCore.Core;

namespace TryAspNetCore.EventManagement
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
        public virtual ICollection<EventRegistration> Registrations { get; set; }
    }
}
