using System.Threading.Tasks;
using FluentValidation.Validation.Models.Results;

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
        /// Validates the model. Override to implement custom validation logic.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <returns>The validation result.</returns>
        protected abstract ValidationResult ValidateModel(TModel model);

        /// <summary>
        /// Validates the model asynchronous. Override to implement custom asynchronous validation logic.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <returns>The validation result.</returns>
        protected abstract Task<ValidationResult> ValidateModelAsync(TModel model);

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
        /// Validates the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<ValidationResult> ValidateAsync(TModel model)
        {
            return await ValidateModelAsync(model);
        }
    }
}
