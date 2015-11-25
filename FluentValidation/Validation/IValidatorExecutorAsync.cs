using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation
{
  public interface IValidatorExecutorAsync<out TModel>
  {
    Task<IEnumerable<ValidationResult>> ExecuteAsync(IEnumerable<IValidatorAsync<TModel>> validators);
  }
}
