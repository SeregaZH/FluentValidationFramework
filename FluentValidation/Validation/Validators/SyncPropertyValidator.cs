using System;
using FluentValidation.Validation.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FluentValidation.Validation.Validators
{
    /// <summary>
    /// Base class for sunchronous property validator.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <seealso cref="FluentValidation.Validation.Validators.PropertyValidator{TModel, TValue}" />
    public abstract class SyncPropertyValidator<TModel, TValue> : PropertyValidator<TModel, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncPropertyValidator{TModel, TValue}"/> class.
        /// </summary>
        /// <param name="descriptor">The validator descriptor <see cref="ValidatorDescriptor" /> (nested <see cref="Validator{TModel}" />).</param>
        /// <param name="propertyGetter">The property value getter.</param>
        protected SyncPropertyValidator(Expression<Func<TModel, TValue>> propertyGetter)
            : base(propertyGetter)
        {
        }

        /// <summary>
        /// Validates the particular property in the model asynchronously.
        /// Override to implement custom asynchronous validation logic for particular property
        /// </summary>
        /// <param name="value">The property value to validate.</param>
        /// <param name="context">The validation context. The entire model.</param>
        /// <param name="propertyName">Name of the property to validate.</param>
        /// <returns>
        /// Property validation result <see cref="PropertyValidationResult" />.
        /// Specific validation result which include property name.
        /// </returns>
        protected sealed override Task<PropertyValidationResult> ValidatePropertyAsync(TValue value, TModel context, string propertyName)
        {
            return Task.FromResult(ValidateProperty(value, context, propertyName));
        }
    }
}
