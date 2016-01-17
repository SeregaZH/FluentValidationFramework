using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;
using LazyValValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, FluentValidation.Validation.Models.ValidationValue, string>>;

namespace FluentValidation.Validation.Validators
{
    public class DeniedValuesValidator<TModel, TValue> : BaseValueValidator<TModel, TValue>
    {
        public DeniedValuesValidator(
            LazyValValidationDescriptor lazyValueDescriptor, 
            Expression<Func<TModel, TValue>> propertyGetter, 
            ValueValidationOptions<TValue> options) 
             : base(lazyValueDescriptor, propertyGetter, options)
        {
        }

        protected override PropertyValidationResult ValidateValue(TValue value, TModel context, string propertyName)
        {
            var validationResult = Options.Values.Contains(value, Options.Comparer);
            return new PropertyValidationResult(!validationResult, DescriptorResolver.Resolve(propertyName, value, LazyValueDescriptor), propertyName);
        }
    }
}
