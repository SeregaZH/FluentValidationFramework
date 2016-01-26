using System;

namespace FluentValidationFramework.Validation.Models
{
    public abstract class BaseValidatorDescriptor
    {
        public BaseValidatorDescriptor(Guid id, string key)
        {
            Id = id;
            Key = key;
        }

        /// <summary>
        /// Gets the validator identifier.
        /// </summary>
        /// <value>
        /// The validator identifier.
        /// Each validator instance should have unique identifier.
        /// </value>
        public Guid Id { get; private set; }

        public string Key { get; private set; }
    }
}
