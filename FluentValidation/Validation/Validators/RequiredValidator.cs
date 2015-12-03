using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    public class RequiredValidator<TModel, TProperty> : PropertyValidator<TModel, TProperty>
    {
        public RequiredValidator(
            ValidatorDescriptor descriptor,
            int priority,
            Expression<Func<TModel, TProperty>> propertyGetter)
            : base(descriptor, priority, propertyGetter)
        {
        }

        protected override PropertyValidationResult ValidateProperty(TProperty property, TModel context,
            string propertyName)
        {
            throw new NotImplementedException(propertyName);
        }
    }
}
