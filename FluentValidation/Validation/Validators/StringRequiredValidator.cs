using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    /// <summary>
    /// String required validator.
    /// Validate any string property on required. 
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="RequiredValidator{TModel, System.String}" />
    public sealed class StringRequiredValidator<TModel> : RequiredValidator<TModel, string>
    {
        private readonly StringValidatorOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringRequiredValidator{TModel}"/> class.
        /// </summary>
        /// <param name="descriptor">The validator descriptor <see cref="ValidatorDescriptor"/> (nested <see cref="RequiredValidator{TModel, string}" />).</param>
        /// <param name="priority">The validator priority (nested <see cref="RequiredValidator{TModel, TValue}" />).</param>
        /// <param name="propertyGetter">The property getter (nested <see cref="RequiredValidator{TModel, TValue}" />).</param>
        /// <param name="invalidValues">The invalid values (nested <see cref="RequiredValidator{TModel, TValue}" />).</param>
        /// <param name="options">The string required options <see cref="StringValidatorOptions" />.</param>
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

        /// <summary>
        /// Validate property.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="context">The validation context.</param>
        /// <param name="propertyName">Name of the validatable property.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override PropertyValidationResult ValidateProperty(string value, TModel context,
            string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}
