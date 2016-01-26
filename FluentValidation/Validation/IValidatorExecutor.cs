using System.Collections.Generic;
using FluentValidationFramework.Validation.Models.Results;
using FluentValidationFramework.Validation.Models;
using System.Threading.Tasks;

namespace FluentValidationFramework.Validation
{
    /// <summary>
    /// Generic interface for all classes which is able to validate model through collection of validators.
    /// </summary>
    /// <typeparam name="TModel">The type of the model to validate.</typeparam>
    public interface IValidatorExecutor<TModel>
        where TModel : class
    {
        /// <summary>
        /// Apply all validators to the model.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <param name="validatorContainers">The validators containers.</param>
        /// <returns>The collection of validation results.</returns>
        IEnumerable<ValidationResult> Execute(TModel model, IEnumerable<ValidatorContainer<TModel>> validatorContainers);

        /// <summary>
        /// Apply all validators to the model asynchronously.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <param name="validators">The validator containers collection.</param>
        /// <returns>Validation results task.</returns>
        Task<IEnumerable<ValidationResult>> ExecuteAsync(TModel model, IEnumerable<ValidatorContainer<TModel>> validatorContainers);
    }
}
