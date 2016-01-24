using System.Threading.Tasks;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    public abstract class SyncValidator<TModel> : Validator<TModel>
    {
        protected sealed override Task<ValidationResult> ValidateModelAsync(TModel model)
        {
            return Task.FromResult(ValidateModel(model));
        }
    }
}
