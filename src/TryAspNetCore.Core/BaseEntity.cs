using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TryAspNetCore.Core
{
    public abstract class BaseEntity
    {
        public abstract Guid Id { get; set; }

        //TODO: Setter must be private
        public Guid CreatedBy { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public Guid UpdatedBy { get; private set; }

        public DateTime UpdatedDate { get; private set; }


        public void SetCreatedInformation(ISessionManager _sessionManager)
        {
            CreatedBy = _sessionManager.Current.UserId; ;
            CreatedDate = DateTime.Now;
        }

        public void SetUpdatedInfirmation(ISessionManager _sessionManager)
        {
            UpdatedBy = _sessionManager.Current.UserId;
            UpdatedDate = DateTime.Now;
        }

        private List<ValidationResult> _validationResults = new List<ValidationResult>();
        public ICollection<ValidationResult> Validate()
        {
            Validator.TryValidateObject(this, new ValidationContext(this), _validationResults);

            return _validationResults;
        }

    }
}