using System.Collections.Generic;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation
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
    }
}
