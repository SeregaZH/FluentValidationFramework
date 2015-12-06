using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation
{
  public interface IValidator<in TModel>
  {
    ValidationResult Validate(TModel model);

    int Priority { get; }
  }
}
