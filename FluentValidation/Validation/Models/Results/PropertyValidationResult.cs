namespace FluentValidation.Validation.Models.Results
{
    public sealed class PropertyValidationResult : ValidationResult
    {
        public PropertyValidationResult(bool isValid, ValidatorDescriptor validatorDescriptor, string propertyName)
            : base(isValid, validatorDescriptor)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }
    }
}
