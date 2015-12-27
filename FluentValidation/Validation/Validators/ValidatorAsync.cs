using System.Threading.Tasks;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
  public abstract class ValidatorAsync<TModel> : IValidatorAsync<TModel>
  {
    protected ValidatorAsync(ValidatorDescriptor descriptor)
    {
      Descriptor = descriptor;
    }

    protected abstract Task<ValidationResult> ValidateModelAsync(TModel model);

    public ValidatorDescriptor Descriptor { get; private set; }


    public async Task<ValidationResult> ValidateAsync(TModel model)
    {
      return await ValidateModelAsync(model);
    }
  }
}
