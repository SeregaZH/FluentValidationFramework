using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    public abstract class PropertyValidator<TModel, TValue> : Validator<TModel>
    {
        private readonly Expression<Func<TModel, TValue>> _propertyGetter;
        private readonly Func<TModel, TValue> _valueResolver;

        protected PropertyValidator(ValidatorDescriptor descriptor, int priority,
            Expression<Func<TModel, TValue>> propertyGetter)
            : base(descriptor, priority)
        {
            _propertyGetter = propertyGetter;
            _valueResolver = propertyGetter.Compile();
        }

        protected sealed override ValidationResult ValidateModel(TModel model)
        {
            return ValidateProperty(_valueResolver(model), model, ResolvePropertyName(_propertyGetter));
        }

        protected abstract PropertyValidationResult ValidateProperty(TValue value, TModel context, string propertyName);

        private string ResolvePropertyName(Expression<Func<TModel, TValue>> propertyGetter)
        {
            var memberExpr = propertyGetter.Body as MemberExpression;

            var propertyInfo = memberExpr?.Member as PropertyInfo;
            
            if (propertyInfo != null)
            {
                return propertyInfo.Name;
            }

            throw new ArgumentException("Expression is not refered to property");
        }
    }
}
