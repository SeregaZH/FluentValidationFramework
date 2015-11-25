using System.Collections.Generic;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation
{
  public interface IValidatorExecutor<out TModel>
  {
    IEnumerable<ValidationResult> Execute(IEnumerable<IValidator<TModel>> validators);
  }
}
