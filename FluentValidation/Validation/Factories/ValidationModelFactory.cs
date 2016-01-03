using FluentValidation.Helpers;
using FluentValidation.Validation.Builders.Fluent;
using FluentValidation.Validation.Configuration;
using FluentValidation.Validation.Fluent;
using FluentValidation.Validation.ValidationModel;
using System;
using System.Collections.Generic;

namespace FluentValidation.Validation.Factories
{
    public class ValidationModelFactory : IValidationModelFactory
    {
        private readonly IDictionary<string, ValidatorsConfigBase> _validatorsConfigContainer;

        public ValidationModelFactory()
        {
            _validatorsConfigContainer = new Dictionary<string, ValidatorsConfigBase>();
        }

        public IValidationModelFactory RegisterConfig<TModel>(Func<IValidatorsConfigBuilder<TModel>, ValidatorsConfig<TModel>> registrator)
            where TModel: class
        {
            Guard.ArgumentNull(registrator, nameof(registrator));

            var config = registrator(new ValidatorsConfigBuilder<TModel>());
            if (_validatorsConfigContainer.ContainsKey(config.RulesetName))
            {
                _validatorsConfigContainer.Remove(config.RulesetName);
            }

            _validatorsConfigContainer.Add(config.RulesetName, config);
            return this;
        }

        public IValidationModel<TModel> ResolveModel<TModel>(string rulesetName = Constants.DefaultRulestName)
            where TModel : class
        {
            if (!_validatorsConfigContainer.ContainsKey(rulesetName))
            {
                throw new ArgumentOutOfRangeException(nameof(rulesetName), "Ruleset doesn't exist");
            }

            var config = (ValidatorsConfig<TModel>)_validatorsConfigContainer[rulesetName];
            return new GenericValidationModel<TModel>(
                config.ValidatorExecutorsConfig.Executor,
                config.ValidatorExecutorsConfig.ExecutorAsync,
                config.ValidationModelConfig);
        }
    }
}
