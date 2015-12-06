using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    public abstract class BaseValueValidator<TModel, TProperty> : PropertyValidator<TModel, TProperty>
    {
        protected BaseValueValidator
            (ValidatorDescriptor descriptor, 
            int priority, 
            Expression<Func<TModel, TProperty>> propertyGetter,
            ValueValidationOptions<TProperty> options) 
            : base(descriptor, priority, propertyGetter)
        {
            Options = options;
        }

        protected ValueValidationOptions<TProperty> Options { get; private set; }

        protected override PropertyValidationResult ValidateProperty(TProperty property, TModel context, string propertyName)
        {
            return ValidateValue(property, context, propertyName);
        }

        protected abstract PropertyValidationResult ValidateValue(TProperty property, TModel context, string propertyName);
    }
}
