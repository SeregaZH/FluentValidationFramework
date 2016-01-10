using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Helpers;

namespace FluentValidation.Validation.Executors
{
    /// <summary>
    /// Perform any collection of validators against the validation model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="IValidatorExecutor{TModel}" />
    public abstract class BaseValidatorExecutor<TModel> : IValidatorExecutor<TModel>
        where TModel : class
    {
        /// <summary>
        /// Apply all validators to the model.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <param name="validatorContainers">The validator containers collection.</param>
        /// <returns>
        /// The collection of validation results.
        /// </returns>
        public IEnumerable<ValidationResult> Execute(TModel model, IEnumerable<ValidatorContainer<TModel>> validatorContainers)
        {
            Guard.ArgumentNull(model, nameof(model));
            Guard.ArgumentNull(validatorContainers, nameof(validatorContainers));
            return PrepareValidators(validatorContainers)
                .Select(validator => validator.Validator.Validate(model));

        }

        /// <summary>
        /// Apply all validators to the model asynchronously.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <param name="validators">The validators collection.</param>
        /// <returns>Validation results task.</returns>
        public async Task<IEnumerable<ValidationResult>> ExecuteAsync(TModel model, IEnumerable<ValidatorContainer<TModel>> validatorContainers)
        {
            Guard.ArgumentNull(model, nameof(model));
            Guard.ArgumentNull(validatorContainers, nameof(validatorContainers));
            return await Task.WhenAll(
                PrepareValidators(validatorContainers)
                .Select(async x => await x.Validator.ValidateAsync(model)));
        }

        /// <summary>
        /// Prepares the validators.
        /// </summary>
        /// <param name="sourceValidators">The source validators.</param>
        /// <returns>Transformed collection of validators.</returns>
        protected abstract IEnumerable<ValidatorContainer<TModel>> PrepareValidators(IEnumerable<ValidatorContainer<TModel>> sourceValidators);
    }
}
