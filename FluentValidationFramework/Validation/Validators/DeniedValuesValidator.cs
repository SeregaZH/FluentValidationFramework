using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidationFramework.Validation.Models;
using FluentValidationFramework.Validation.Models.Options;
using LazyValValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, FluentValidationFramework.Validation.Models.ValidationValue, string>>;

namespace FluentValidationFramework.Validation.Validators
{
    public class DeniedValuesValidator<TModel, TValue> : BaseValueValidator<TModel, TValue>
    {
        public DeniedValuesValidator(
            LazyValValidationDescriptor lazyValueDescriptor, 
            Expression<Func<TModel, TValue>> propertyGetter,
            IValueValidatorOptions<TValue> options) 
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
