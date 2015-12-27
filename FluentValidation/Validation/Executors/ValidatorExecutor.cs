using System.Collections.Generic;
using System.Linq;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Helpers;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Executors
{
    /// <summary>
    /// Perform plain collection of validators on the validation model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="IValidatorExecutor{TModel}" />
    public class ValidatorExecutor<TModel> : IValidatorExecutor<TModel> 
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
            return validatorContainers.OrderBy(x => x.Priority).Select(validator => validator.Validator.Validate(model));
        }
    }
}
