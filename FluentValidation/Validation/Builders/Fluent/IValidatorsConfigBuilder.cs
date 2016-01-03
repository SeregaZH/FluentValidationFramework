using FluentValidation.Validation.Builders.Fluent;
using FluentValidation.Validation.Configuration;
using System;

namespace FluentValidation.Validation.Fluent
{
    public interface IValidatorsConfigBuilder<TModel>
        where TModel : class
    {
        IValidatorsConfigBuilder<TModel> WithValidationModel(Func<IValidationModelConfigBuilder<TModel>,ValidationModelConfig<TModel>> configBuilder);

        IValidatorsConfigBuilder<TModel> WithValidatorExecutors(Func<IValidatorExecutorsConfigBuilder<TModel>, ValidatorExecutorsConfig<TModel>> configBuilder);

        ValidatorsConfig<TModel> Build(string rulesetName = Constants.DefaultRulestName);
    }
}
