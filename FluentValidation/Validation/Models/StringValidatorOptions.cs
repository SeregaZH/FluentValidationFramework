using System;

namespace FluentValidation.Validation.Models
{
    public sealed class StringValidatorOptions
    {
        public StringValidatorOptions(bool isTrimmed, StringComparison comparationType)
        {
            IsTrimmed = isTrimmed;
            ComparationType = comparationType;
        }

        public bool IsTrimmed { get; }

        public StringComparison ComparationType { get; }
    }
}
