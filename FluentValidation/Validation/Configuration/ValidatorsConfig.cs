using System;

namespace FluentValidation.Validation.Configuration
{
    public sealed class ValidatorsConfig<TModel>
        where TModel : class
    {
        public ValidatorsConfig(
            string rulesetName,
            ValidationModelConfig<TModel> validationModelConfig,
            ValidatorExecutorsConfig<TModel> validatorExecutorsConfig)
        {
            RulesetName = rulesetName;
            ValidationModelConfig = validationModelConfig;
            ValidatorExecutorsConfig = validatorExecutorsConfig;
        }

        public string RulesetName { get; private set; }

        public Type TargetType { get { return typeof(TModel); } }

        public ValidationModelConfig<TModel> ValidationModelConfig { get; private set; }

        public ValidatorExecutorsConfig<TModel> ValidatorExecutorsConfig { get; private set; }
    }
}
