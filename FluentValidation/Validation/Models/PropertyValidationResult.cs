namespace FluentValidation.Validation.Models
{
    /// <summary>
    /// Specific validation result for property validators
    /// </summary>
    /// <seealso cref="ValidationResult" />
    /// <seealso cref="Validators.PropertyValidator{TModel, TValue}" />
    public sealed class PropertyValidationResult : ValidationResult
    {
        public PropertyValidationResult(bool isValid, ValidatorDescriptor validatorDescriptor, string propertyName)
            : base(isValid, validatorDescriptor)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the name of the property to validate.
        /// </summary>
        /// <value>
        /// The name of the property to validate.
        /// </value>
        public string PropertyName { get; private set; }
    }
}
