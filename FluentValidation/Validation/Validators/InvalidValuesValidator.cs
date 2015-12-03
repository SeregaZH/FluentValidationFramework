using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    public class InvalidValuesValidator<TModel, TProperty> : PropertyValidator<TModel, TProperty>
    {
        private readonly HashSet<TProperty> _invalidValues;

        public InvalidValuesValidator(ValidatorDescriptor descriptor, int priority, Expression<Func<TModel, TProperty>> propertyGetter, HashSet<TProperty> invalidValues) 
            : base(descriptor, priority, propertyGetter)
        {
            _invalidValues = invalidValues;
        }

        protected override PropertyValidationResult ValidateProperty(TProperty property, TModel context, string propertyName)
        {
            return _invalidValues.Contains(property) 
                ? new PropertyValidationResult(false, Descriptor, propertyName) 
                : new PropertyValidationResult(true, Descriptor, propertyName);
        }
    }
}
