using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    public class RequiredValidator<TModel, TValue> : PropertyValidator<TModel, TValue>
    {
        private readonly HashSet<TValue> _invalidValues;

        public RequiredValidator(
            ValidatorDescriptor descriptor,
            int priority,
            Expression<Func<TModel, TValue>> propertyGetter,
            HashSet<TValue> invalidValues)
            : base(descriptor, priority, propertyGetter)
        {
            _invalidValues = invalidValues;
        }

        protected override PropertyValidationResult ValidateProperty(TValue value, TModel context,
            string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
