using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    public class RequiredValidator<TModel, TProperty> : PropertyValidator<TModel, TProperty>
    {
        private readonly HashSet<TProperty> _invalidValues;

        public RequiredValidator(
            ValidatorDescriptor descriptor,
            int priority,
            Expression<Func<TModel, TProperty>> propertyGetter,
            HashSet<TProperty> invalidValues)
            : base(descriptor, priority, propertyGetter)
        {
            _invalidValues = invalidValues;
        }

        protected override PropertyValidationResult ValidateProperty(TProperty property, TModel context,
            string propertyName)
        {
            throw new NotImplementedException(propertyName);
        }
    }
}
