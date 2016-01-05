using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    /// <summary>
    /// Required validator.
    /// </summary>
    /// <typeparam name="TModel">The type of the model to vaildate.</typeparam>
    /// <typeparam name="TValue">The type of the property value to validate.</typeparam>
    /// <seealso cref="PropertyValidator{TModel, TValue}" />
    public class RequiredValidator<TModel, TValue> : PropertyValidator<TModel, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredValidator{TModel, TValue}"/> class.
        /// </summary>
        /// <param name="descriptor">The validator descriptor <see cref="ValidatorDescriptor" /> (nested <see cref="PropertyValidator{TModel, TValue}" />).</param>
        /// <param name="propertyGetter">The property getter (nested <see cref="PropertyValidator{TModel, TValue}" />).</param>
        /// <param name="invalidValues">The set of invalid values.</param>
        public RequiredValidator(
            ValidatorDescriptor descriptor,
            Expression<Func<TModel, TValue>> propertyGetter)
            : base(descriptor, propertyGetter)
        {
        }

        /// <summary>
        /// Validates the particular property in the model.
        /// Override to implement custom validation logic for particular property
        /// </summary>
        /// <param name="value">The property value to validate.</param>
        /// <param name="context">The validation context. The entire model.</param>
        /// <param name="propertyName">Name of the property to validate.</param>
        /// <returns>
        /// Property validation result <see cref="PropertyValidationResult" />.
        /// Specific validation result which include property name.
        /// </returns>
        protected override PropertyValidationResult ValidateProperty(TValue value, TModel context,
            string propertyName)
        {
            var validationResult = Equals(value, default(TValue));
            return new PropertyValidationResult(!validationResult, Descriptor, propertyName);
        }
    }
}
