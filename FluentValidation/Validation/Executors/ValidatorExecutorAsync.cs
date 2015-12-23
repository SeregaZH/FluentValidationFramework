using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Helpers;
using System.Linq;

namespace FluentValidation.Validation.Executors
{
    /// <summary>
    /// Asynchronous Generic interface for all classes which is able to validate model through collection of validators.
    /// </summary>
    public class ValidatorExecutorAsync<TModel> : IValidatorExecutorAsync<TModel>
        where TModel : class
    {
        /// <summary>
        /// Apply all validators to the model asynchronously.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <param name="validators">The validators collection.</param>
        /// <returns>Validation results task.</returns>
        public IEnumerable<Task<ValidationResult>> ExecuteAsync(TModel model, IEnumerable<IValidatorAsync<TModel>> validators)
        {
            Guard.ArgumentNull(model, nameof(model));
            Guard.ArgumentNull(validators, nameof(validators));
            return validators.Select(async x => await x.ValidateAsync(model));
        }
    }
}
