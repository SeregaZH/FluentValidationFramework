using FluentValidation.Validation.Configuration;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Fluent
{
    public interface IValidationModelConfigBuilder<TModel>
    {
        ValidationModelConfig<TModel> Build(TModel model);

        IValidationModelConfigBuilder<TModel> AddValidator(ValidatorContainer<TModel> validatorContainer);

        IValidationModelConfigBuilder<TModel> AddAsyncValidator(ValidatorContainerAsync<TModel> asyncValidatorContainer);
    }
}
