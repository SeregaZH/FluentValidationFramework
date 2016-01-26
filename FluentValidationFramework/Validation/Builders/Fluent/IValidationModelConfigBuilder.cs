using FluentValidationFramework.Validation.Configuration;
using FluentValidationFramework.Validation.Models;

namespace FluentValidationFramework.Validation.Fluent
{
    public interface IValidationModelConfigBuilder<TModel>
    {
        ValidationModelConfig<TModel> Build();

        IValidationModelConfigBuilder<TModel> AddValidator(ValidatorContainer<TModel> validatorContainer);
    }
}
