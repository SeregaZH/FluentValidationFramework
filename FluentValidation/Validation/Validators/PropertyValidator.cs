﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    /// <summary>
    /// Base class for property validators.
    /// Implement basic functionality which each property validator should have.
    /// Override to implement custom property validator.
    /// </summary>
    /// <typeparam name="TModel">The type of the model to validate.</typeparam>
    /// <typeparam name="TValue">The type of the property value to validate.</typeparam>
    /// <seealso cref="Validator{TModel}" />
    public abstract class PropertyValidator<TModel, TValue> : Validator<TModel>
    {
        private readonly Expression<Func<TModel, TValue>> _propertyGetter;
        private readonly Func<TModel, TValue> _valueResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyValidator{TModel, TValue}"/> class.
        /// </summary>
        /// <param name="descriptor">The validator descriptor <see cref="ValidatorDescriptor" /> (nested <see cref="Validator{TModel}" />).</param>
        /// <param name="priority">The validator priority (nested <see cref="Validator{TModel}" />).</param>
        /// <param name="propertyGetter">The property value getter.</param>
        protected PropertyValidator(ValidatorDescriptor descriptor, int priority,
            Expression<Func<TModel, TValue>> propertyGetter)
            : base(descriptor, priority)
        {
            _propertyGetter = propertyGetter;
            _valueResolver = propertyGetter.Compile();
        }

        /// <summary>
        /// Validates the model. Override to implement custom validation logic.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <returns></returns>
        protected sealed override ValidationResult ValidateModel(TModel model)
        {
            return ValidateProperty(_valueResolver(model), model, ResolvePropertyName(_propertyGetter));
        }

        /// <summary>
        /// Validates the particular property in the model. 
        /// Override to implement custom validation logic for particular property 
        /// </summary>
        /// <param name="value">The property value to validate.</param>
        /// <param name="context">The validation context. The entire model.</param>
        /// <param name="propertyName">Name of the property to validate.</param>
        /// <exception cref="System.ArgumentException">Expression is not refered to property.</exception>
        /// <returns>
        /// Property validation result <see cref="PropertyValidationResult"/>.
        /// Specific validation result which include property name.
        /// </returns>
        protected abstract PropertyValidationResult ValidateProperty(TValue value, TModel context, string propertyName);


        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyGetter">The property getter.</param>
        /// <returns>Property name.</returns>
        /// <exception cref="System.ArgumentException">Expression is not refered to property</exception>
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
