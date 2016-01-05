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
        private readonly StringValuesValidatorOptions _stringOptions;

        public DeniedStringValuesValidator(ValidatorDescriptor descriptor, Expression<Func<TModel, string>> propertyGetter, StringValuesValidatorOptions options) 
            : base(descriptor, propertyGetter, new ValueValidationOptions<string>(new HashSet<string>(options.Values.Select(x => options.IsTrimmed ? x?.Trim() : x)), options.Comparer))
        {
            _stringOptions = options;
        }

        protected override PropertyValidationResult ValidateValue(string value, TModel context, string propertyName)
        {
            return base.ValidateValue(_stringOptions.IsTrimmed ? value?.Trim() : value, context, propertyName);
        }
    }
}
