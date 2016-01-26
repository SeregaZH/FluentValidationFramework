using System.Collections.Generic;
using System.Linq;
using FluentValidationFramework.Validation.Models;

namespace FluentValidationFramework.Validation.Executors
{
    /// <summary>
    /// Perform plain collection of validators against the validation model.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <seealso cref="BaseValidatorExecutor{TModel}" />
    public sealed class PlainValidatorExecutor<TModel> : BaseValidatorExecutor<TModel>
        where TModel : class
    {
        /// <summary>
        /// Prepares the plain collection of validators.
        /// </summary>
        /// <param name="sourceValidators">The source validators.</param>
        /// <returns>
        /// Transformed collection of validators.
        /// </returns>
        protected override IEnumerable<ValidatorContainer<TModel>> PrepareValidators(IEnumerable<ValidatorContainer<TModel>> sourceValidators)
        {
            return sourceValidators.OrderBy(x => x.Priority);
        }
    }
}
