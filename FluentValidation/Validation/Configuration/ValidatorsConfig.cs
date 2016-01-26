using System;

namespace FluentValidationFramework.Validation.Configuration
{
    public sealed class ValidatorsConfig<TModel> : ValidatorsConfigBase
        where TModel : class
    {
        public ValidatorsConfig(
            string rulesetName,
            ValidationModelConfig<TModel> validationModelConfig,
            ValidatorExecutorsConfig<TModel> validatorExecutorsConfig)
            :base(rulesetName)
        {
            ValidationModelConfig = validationModelConfig;
            ValidatorExecutorsConfig = validatorExecutorsConfig;
        }

        public Type TargetType { get { return typeof(TModel); } }

        public ValidationModelConfig<TModel> ValidationModelConfig { get; private set; }

        public ValidatorExecutorsConfig<TModel> ValidatorExecutorsConfig { get; private set; }
    }
}
