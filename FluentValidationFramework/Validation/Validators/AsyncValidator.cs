using FluentValidationFramework.Validation.Models.Results;
using FluentValidationFramework.Helpers;

namespace FluentValidationFramework.Validation.Validators
{
    public abstract class AsyncValidator<TModel> : Validator<TModel>
    {
        protected sealed override ValidationResult ValidateModel(TModel model)
        {
            using (var syncWaiter = AsyncHelper.Sync())
            {
                return syncWaiter.RunSync(() => ValidateModelAsync(model));
            }
        }
    }
}
