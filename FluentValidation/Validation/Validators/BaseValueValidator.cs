using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;
using LazyValValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, FluentValidation.Validation.Models.ValidationValue, string>>;

namespace FluentValidation.Validation.Validators
{
    public abstract class BaseValueValidator<TModel, TValue> : SyncPropertyValidator<TModel, TValue>
    {
        protected BaseValueValidator
            (LazyValValidationDescriptor lazyDescriptor, 
            Expression<Func<TModel, TValue>> propertyGetter,
            IValueValidatorOptions<TValue> options) 
            : base(propertyGetter)
        {
            Options = options;
            LazyValueDescriptor = lazyDescriptor;
        }

        protected LazyValValidationDescriptor LazyValueDescriptor { get; private set; }

        protected IValueValidatorOptions<TValue> Options { get; private set; }

        protected override PropertyValidationResult ValidateProperty(TValue value, TModel context, string propertyName)
        {
            return ValidateValue(value, context, propertyName);
        }

        protected abstract PropertyValidationResult ValidateValue(TValue property, TModel context, string propertyName);
    }
}
