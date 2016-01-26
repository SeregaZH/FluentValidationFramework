using FluentValidationFramework.Validation.Models;
using FluentValidationFramework.Validation.Models.Results;
using System.Threading.Tasks;

namespace FluentValidationFramework.Validation
{
    /// <summary>
    /// Interface for synchronous validators. 
    /// </summary>
    /// <typeparam name="TModel">The type of the model to validate.</typeparam>
    public interface IValidator<in TModel>
    {
        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <returns>Validation result <see cref="ValidationResult"/>.</returns>
        ValidationResult Validate(TModel model);

        Task<ValidationResult> ValidateAsync(TModel model);
    }
}
