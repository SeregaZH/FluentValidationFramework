using System;
using System.Linq.Expressions;
using FluentValidationFramework.Validation.Models;
using FluentValidationFramework.Validation.Models.Options;
using LazyValValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, FluentValidationFramework.Validation.Models.ValidationValue, string>>;

namespace FluentValidationFramework.Validation.Validators
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
