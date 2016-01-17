using System;

namespace FluentValidation.Validation.Models
{
    /// <summary>
    /// The validator descriptor.
    /// Described basic validator properties.
    /// </summary>
    public sealed class ValidatorDescriptor : BaseValidatorDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorDescriptor"/> class.
        /// </summary>
        /// <param name="id">The validator identifier.</param>
        /// <param name="key">The validator key.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="description">The validator description.</param>
        public ValidatorDescriptor
            (Guid id, string key, string errorMessage, string description)
            : base(id, key)
        {
            ErrorMessage = errorMessage;
            Description = description;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Gets the validator description.
        /// </summary>
        /// <value>
        /// The validator description.
        /// Some validator description.
        /// </value>
        public string Description { get; private set; }
    }
}
