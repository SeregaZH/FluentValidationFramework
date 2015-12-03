using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    public sealed class StringRequiredValidator<TModel> : RequiredValidator<TModel, string>
    {
        private readonly StringValidatorOptions _options;

        public StringRequiredValidator(
            ValidatorDescriptor descriptor,
            int priority,
            Expression<Func<TModel, string>> propertyGetter,
            HashSet<string> invalidValues,
            StringValidatorOptions options)
            : base(descriptor, priority, propertyGetter, invalidValues)
        {
            _options = options;
        }

        protected override PropertyValidationResult ValidateProperty(string property, TModel context,
            string propertyName)
        {
            return base.ValidateProperty(property, context, propertyName);
        }
    }
}
