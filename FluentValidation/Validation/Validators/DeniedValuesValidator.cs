using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    public class DeniedValuesValidator<TModel, TProperty> : BaseValueValidator<TModel, TProperty>
    {
        public DeniedValuesValidator(
            ValidatorDescriptor descriptor, 
            Expression<Func<TModel, TProperty>> propertyGetter, 
            ValueValidationOptions<TProperty> options) 
             : base(descriptor, propertyGetter, options)
        {

        }

        protected override PropertyValidationResult ValidateValue(TProperty property, TModel context, string propertyName)
        {
            return Options.Values.Contains(property, Options.Comparer) 
                ? new PropertyValidationResult(false, Descriptor, propertyName) 
                : new PropertyValidationResult(true, Descriptor, propertyName);
        }
    }
}
