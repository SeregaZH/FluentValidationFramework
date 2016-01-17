using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using LazyPropValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, string>>;

namespace FluentValidation.Validation.Validators
{
    /// <summary>
    /// Required validator.
    /// </summary>
    /// <typeparam name="TModel">The type of the model to vaildate.</typeparam>
    /// <typeparam name="TValue">The type of the property value to validate.</typeparam>
    /// <seealso cref="SyncPropertyValidator{TModel, TValue}" />
    public class RequiredValidator<TModel, TValue> : SyncPropertyValidator<TModel, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredValidator{TModel, TValue}"/> class.
        /// </summary>
        /// <param name="descriptor">The validator descriptor <see cref="ValidatorDescriptor" /> (nested <see cref="PropertyValidator{TModel, TValue}" />).</param>
        /// <param name="propertyGetter">The property getter (nested <see cref="SyncPropertyValidator{TModel, TValue}" />).</param>
        /// <param name="invalidValues">The set of invalid values.</param>
        public RequiredValidator(
            LazyPropValidationDescriptor lazyValidatorDescriptor,
            Expression<Func<TModel, TValue>> propertyGetter):
            base(propertyGetter)
        {
            LazyValidatorDescriptor = lazyValidatorDescriptor;
        }

        protected LazyPropValidationDescriptor LazyValidatorDescriptor { get; private set; }

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
            return new PropertyValidationResult(!validationResult, DescriptorResolver.Resolve(propertyName, LazyValidatorDescriptor), propertyName);
        }
    }
}
