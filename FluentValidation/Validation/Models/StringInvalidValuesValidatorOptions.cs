using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.Validation.Models
{
    public sealed class StringInvalidValuesValidatorOptions : BaseStringValidationOptions
    {
        public StringInvalidValuesValidatorOptions(bool isTrimmed, StringComparer comparer) 
            : base(isTrimmed)
        {
            Comparer = comparer;
        }

        public StringComparer Comparer { get; }
    }
}
