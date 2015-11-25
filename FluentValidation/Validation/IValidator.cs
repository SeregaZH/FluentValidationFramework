using FluentValidation.Validation.Models;

namespace FluentValidation.Validation
{
  public interface IValidator<in TModel>
  {
    ValidationResult Validate(TModel model);

    int Priority { get; }
  }
}
