using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;

namespace FluentValidation.Validation.Validators
{
    public sealed class DeniedStringValuesValidator<TModel> : DeniedValuesValidator<TModel, string>
    {
        public DeniedStringValuesValidator(ValidatorDescriptor descriptor, Expression<Func<TModel, string>> propertyGetter, HashSet<string> deniedValues, StringValuesValidatorOptions options) 
            : base(descriptor, propertyGetter, new ValueValidationOptions<string>(new HashSet<string>(deniedValues.Select(x => options.IsTrimmed ? x?.Trim() : x)), options.Comparer))
        {
            
        }
    }
}
