using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    public abstract class BaseValueValidator<TModel, TValue> : PropertyValidator<TModel, TValue>
    {
        protected BaseValueValidator
            (ValidatorDescriptor descriptor, 
            Expression<Func<TModel, TValue>> propertyGetter,
            ValueValidationOptions<TValue> options) 
            : base(descriptor, propertyGetter)
        {
            Options = options;
        }

        protected ValueValidationOptions<TValue> Options { get; private set; }

        protected override PropertyValidationResult ValidateProperty(TValue value, TModel context, string propertyName)
        {
            return ValidateValue(value, context, propertyName);
        }

        protected abstract PropertyValidationResult ValidateValue(TValue property, TModel context, string propertyName);
    }
}
