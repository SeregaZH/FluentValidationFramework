using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidationFramework.Validation.Models;
using FluentValidationFramework.Validation.Models.Options;
using LazyValValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, FluentValidationFramework.Validation.Models.ValidationValue, string>>;

namespace FluentValidationFramework.Validation.Validators
{
    public sealed class DeniedStringValuesValidator<TModel> : DeniedValuesValidator<TModel, string>
    {
        private readonly StringValuesValidatorOptions _stringOptions;

        public DeniedStringValuesValidator(
            LazyValValidationDescriptor lazyValueDescriptor,
            Expression<Func<TModel, string>> propertyGetter,
            StringValuesValidatorOptions options) 
            : base(lazyValueDescriptor, propertyGetter, new ValueValidatorOptions<string>(new HashSet<string>(options.Values.Select(x => options.IsTrimmed ? x?.Trim() : x)), options.Comparer))
        {
            _stringOptions = options;
        }

        protected override PropertyValidationResult ValidateValue(string value, TModel context, string propertyName)
        {
            return base.ValidateValue(_stringOptions.IsTrimmed ? value?.Trim() : value, context, propertyName);
        }
    }
}
