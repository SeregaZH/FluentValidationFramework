using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    /// <summary>
    /// Base class for validators. 
    /// Implement basic functionality which each validator should have.
    /// Use as base class to implement custom validation logic to entire model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model to validate.</typeparam>
    /// <seealso cref="IValidator{TModel}" />
    public abstract class Validator<TModel> : IValidator<TModel>
    {
        /// <summary>
        /// Initializes a part of the <see cref="Validator{TModel}"/> class.
        /// </summary>
        /// <param name="descriptor">The validator descriptor <see cref="ValidatorDescriptor" />.</param>
        /// <param name="priority">The validator priority.</param>
        protected Validator(ValidatorDescriptor descriptor, int priority)
        {
            Descriptor = descriptor;
            Priority = priority;
        }

        /// <summary>
        /// Validates the model. Override to implement custom validation logic.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <returns></returns>
        protected abstract ValidationResult ValidateModel(TModel model);

        /// <summary>
        /// Gets the validator descriptor <see cref="ValidatorDescriptor">.
        /// </summary>
        /// <value>
        /// The validator descriptor.
        /// </value>
        public ValidatorDescriptor Descriptor { get; private set; }


        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <returns>
        /// Validation result <see cref="ValidationResult" />.
        /// </returns>
        public ValidationResult Validate(TModel model)
        {
            return ValidateModel(model);
        }

        /// <summary>
        /// Gets the validator priority.
        /// </summary>
        /// <value>
        /// The validator priority.
        /// </value>
        public int Priority { get; private set; }
    }
}
