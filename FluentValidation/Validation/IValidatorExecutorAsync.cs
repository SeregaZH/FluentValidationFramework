using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation
{
    /// <summary>
    /// Asynchronous generic interface for all classes which is able to validate model through collection of validators.
    /// </summary>
    /// <typeparam name="TModel">The type of the model to validate.</typeparam>
    public interface IValidatorExecutorAsync<TModel>
        where TModel : class
    {
        /// <summary>
        /// Apply all validators to the model asynchronously.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <param name="validators">The validators collection.</param>
        /// <returns>Validation results task.</returns>
        IEnumerable<Task<ValidationResult>> ExecuteAsync(TModel model, IEnumerable<IValidatorAsync<TModel>> validators);
    }
}
