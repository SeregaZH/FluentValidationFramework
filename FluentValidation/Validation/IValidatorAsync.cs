using System.Threading.Tasks;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation
{
  public interface IValidatorAsync<in TModel>
  {
    Task<ValidationResult> ValidateAsync(TModel model);

    int Priority { get; }
  }
}
