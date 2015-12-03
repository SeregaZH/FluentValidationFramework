using System;

namespace FluentValidation.Validation.Models
{
    public sealed class StringRequiredValidatorOptions : BaseStringValidationOptions
    {
        public StringRequiredValidatorOptions(bool isTrimmed, StringComparison comparationType)
            : base(isTrimmed)
        {
            ComparationType = comparationType;
        }

        public StringComparison ComparationType { get; }
    }
}
