using System.Collections.Generic;
using System.Linq;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Helpers;

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
        /// <param name="validators">The validators collection.</param>
        /// <returns>
        /// The collection of validation results.
        /// </returns>
        public IEnumerable<ValidationResult> Execute(TModel model, IEnumerable<IValidator<TModel>> validators)
        {
            Guard.ArgumentNull(model, nameof(model));
            Guard.ArgumentNull(validators, nameof(validators));
            return validators.Select(validator => validator.Validate(model));
        }
    }
}
