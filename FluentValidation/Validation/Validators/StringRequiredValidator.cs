﻿using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Options;

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
        private readonly BaseStringValidationOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringRequiredValidator{TModel}"/> class.
        /// </summary>
        /// <param name="descriptor">The validator descriptor <see cref="ValidatorDescriptor"/> (nested <see cref="RequiredValidator{TModel, string}" />).</param>
        /// <param name="propertyGetter">The property getter (nested <see cref="RequiredValidator{TModel, TValue}" />).</param>
        /// <param name="invalidValues">The invalid values (nested <see cref="RequiredValidator{TModel, TValue}" />).</param>
        /// <param name="options">The string required options <see cref="BaseStringValidationOptions" />.</param>
        public StringRequiredValidator(
            ValidatorDescriptor descriptor,
            Expression<Func<TModel, string>> propertyGetter,
            BaseStringValidationOptions options)
            : base(descriptor, propertyGetter)
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
            return new PropertyValidationResult(
                !string.IsNullOrEmpty(_options.IsTrimmed ? value?.Trim() : value),
                Descriptor,
                propertyName);
        }
    }
}
