using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation
{
    public interface IValidatorExecutorAsync<out TModel>
    {
        Task<IEnumerable<ValidationResult>> ExecuteAsync(IEnumerable<IValidatorAsync<TModel>> validators);
    }
}
