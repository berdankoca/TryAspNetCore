using System;
using System.ComponentModel.DataAnnotations.Schema;
using TryAspNetCore.Core;

namespace TryAspNetCore.EventManagement
{
    [Table("EventRegistration")]
    public class EventRegistration : BaseEntity
    {
        [Column("EventRegistrationId")]
        public override Guid Id { get; set; }

        public Guid EventId { get; set; }

        public Guid UserId { get; set; }
    }

}