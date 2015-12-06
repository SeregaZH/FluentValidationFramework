using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    public sealed class StringRequiredValidator<TModel> : RequiredValidator<TModel, string>
    {
        private readonly StringRequiredValidatorOptions _options;

        public StringRequiredValidator(
            ValidatorDescriptor descriptor,
            int priority,
            Expression<Func<TModel, string>> propertyGetter,
            StringRequiredValidatorOptions options)
            : base(descriptor, priority, propertyGetter)
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
