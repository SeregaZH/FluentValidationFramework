using System;

namespace FluentValidation.Validation.Configuration
{
    public class ValidatorsConfig<TModel>
    {
        public ValidatorsConfig(
            string rulesetName,
            ValidationModelConfig<TModel> validationModelConfig)
        {
            RulesetName = rulesetName;
            ValidationModelConfig = validationModelConfig;
        }

        public string RulesetName { get; private set; }

        public Type TargetType { get { return typeof(TModel); } }

        public ValidationModelConfig<TModel> ValidationModelConfig { get; private set; }
    }
}
