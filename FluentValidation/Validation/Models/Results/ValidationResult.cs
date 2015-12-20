using System;

namespace FluentValidation.Validation.Models.Results
{
    /// <summary>
    /// The validation result.
    /// Represent result of performing common validation rule.
    /// </summary>
    /// <seealso cref="IValidationResult" />
    /// <seealso cref="Validators.Validator{TModel}" />
    [Serializable]
    public class ValidationResult : IValidationResult
    {
        private readonly ValidatorDescriptor _validatorDescriptor;
        private readonly bool _isValid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="isValid">if set to <c>true</c> validator was valid otherwise validation was failed.</param>
        /// <param name="validatorDescriptor">The validator descriptor.</param>
        public ValidationResult(bool isValid, ValidatorDescriptor validatorDescriptor)
        {
            _validatorDescriptor = validatorDescriptor;
            _isValid = isValid;
        }

        /// <summary>
        /// Returns <c>true</c> if validator is valid, otherwise <c>false</c>.
        /// </summary>
        /// <returns><see cref="bool"/>Validation result.</returns>
        public bool IsValid()
        {
            return _isValid;
        }

        /// <summary>
        /// Gets the validator identifier.
        /// </summary>
        /// <value>
        /// The validator identifier.
        /// Each validator instance should have unique identifier.
        /// </value>
        public Guid Id
        {
            get
            {
                return _validatorDescriptor.Id;
            }
        }

        /// <summary>
        /// Gets the validator key.
        /// </summary>
        /// <value>
        /// The validator key.
        /// Each group of validator should have same key.
        /// </value>
        public string Key
        {
            get
            {
                return _validatorDescriptor.Key;
            }
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage
        {
            get
            {
                return _validatorDescriptor.ErrorMessage;
            }
        }

        /// <summary>
        /// Gets the validator description.
        /// </summary>
        /// <value>
        /// The validator description.
        /// Some validator description.
        /// </value>
        public string Description
        {
            get
            {
                return _validatorDescriptor.Description;
            }
        }
    }
}
