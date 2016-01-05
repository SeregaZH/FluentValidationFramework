using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;

namespace FluentValidation.Validation.Validators
{
    public class DeniedValuesValidator<TModel, TValue> : BaseValueValidator<TModel, TValue>
    {
        public DeniedValuesValidator(
            ValidatorDescriptor descriptor, 
            Expression<Func<TModel, TValue>> propertyGetter, 
            ValueValidationOptions<TValue> options) 
             : base(descriptor, propertyGetter, options)
        {
        }

        protected override PropertyValidationResult ValidateValue(TValue value, TModel context, string propertyName)
        {
            var validationResult = Options.Values.Contains(value, Options.Comparer);
            return new PropertyValidationResult(!validationResult, Descriptor, propertyName);
        }
    }
}
