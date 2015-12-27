using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Helpers;
using System.Linq;
using FluentValidation.Validation.Models;

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
        public async Task<IEnumerable<ValidationResult>> ExecuteAsync(TModel model, IEnumerable<ValidatorContainerAsync<TModel>> validators)
        {
            Guard.ArgumentNull(model, nameof(model));
            Guard.ArgumentNull(validators, nameof(validators));
            return await Task.WhenAll(validators
                .OrderBy(x => x.Priority)
                .Select(async x => await x.Validator.ValidateAsync(model)));
        }
    }
}
