using FluentValidation.Validation.Models.Results;
using FluentValidation.Helpers;

namespace FluentValidation.Validation.Validators
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
