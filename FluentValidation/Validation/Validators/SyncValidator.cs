using System.Threading.Tasks;
using FluentValidationFramework.Validation.Models.Results;

namespace FluentValidationFramework.Validation.Validators
{
    public abstract class SyncValidator<TModel> : Validator<TModel>
    {
        protected sealed override Task<ValidationResult> ValidateModelAsync(TModel model)
        {
            return Task.FromResult(ValidateModel(model));
        }
    }
}
