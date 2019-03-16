using System;

namespace TryAspNetCore.Api.Core
{
    public abstract class BaseEntity
    {
        public abstract Guid Id { get; set; }

        //TODO: Setter must be private
        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}