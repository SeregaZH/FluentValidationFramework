using System;
using System.Collections.Generic;

namespace FluentValidation.Validation.Models.Options
{
    public sealed class StringValuesValidatorOptions : BaseStringValidationOptions, IValueValidatorOptions<string>
    {
        public StringValuesValidatorOptions(
            bool isTrimmed, 
            StringComparer comparer, 
            HashSet<string> values) 
            : base(isTrimmed)
        {
            Comparer = comparer;
            Values = values;
        }

        public IEqualityComparer<string> Comparer { get; }

        public HashSet<string> Values { get; }
    }
}
