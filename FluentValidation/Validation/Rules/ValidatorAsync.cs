using System.Threading.Tasks;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Rules
{
  public abstract class ValidatorAsync<TModel> : IValidatorAsync<TModel>
  {
    protected ValidatorAsync(ValidatorDescriptor descriptor, int priority)
    {
      Descriptor = descriptor;
      Priority = priority;
    }

    protected abstract Task<ValidationResult> ValidateModelAsync(TModel model);

    public ValidatorDescriptor Descriptor { get; private set; }

    public int Priority { get; private set; }

    public async Task<ValidationResult> ValidateAsync(TModel model)
    {
      return await ValidateModelAsync(model);
    }
  }
}
