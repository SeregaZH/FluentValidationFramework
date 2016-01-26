using FluentValidationFramework.Validation.Builders.Fluent;
using FluentValidationFramework.Validation.Configuration;
using System;

namespace FluentValidationFramework.Validation.Fluent
{
    public interface IValidatorsConfigBuilder<TModel>
        where TModel : class
    {
        IValidatorsConfigBuilder<TModel> WithValidationModel(Func<IValidationModelConfigBuilder<TModel>,ValidationModelConfig<TModel>> configBuilder);

        IValidatorsConfigBuilder<TModel> WithValidatorExecutors(Func<IValidatorExecutorsConfigBuilder<TModel>, ValidatorExecutorsConfig<TModel>> configBuilder);

        ValidatorsConfig<TModel> Build(string rulesetName = Constants.DefaultRulestName);
    }
}
