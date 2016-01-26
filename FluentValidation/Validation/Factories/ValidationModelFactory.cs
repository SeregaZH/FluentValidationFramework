using FluentValidationFramework.Helpers;
using FluentValidationFramework.Validation.Builders.Fluent;
using FluentValidationFramework.Validation.Configuration;
using FluentValidationFramework.Validation.Fluent;
using FluentValidationFramework.Validation.ValidationModel;
using System;
using System.Collections.Generic;

namespace FluentValidationFramework.Validation.Factories
{
    public class ValidationModelFactory : IValidationModelFactory
    {
        private readonly IDictionary<Tuple<string, string>, ValidatorsConfigBase> _validatorsConfigContainer;

        public ValidationModelFactory()
        {
            _validatorsConfigContainer = new Dictionary<Tuple<string, string>, ValidatorsConfigBase>();
        }

        public IValidationModelFactory RegisterConfig<TModel>(Func<IValidatorsConfigBuilder<TModel>, ValidatorsConfig<TModel>> registrator)
            where TModel: class
        {
            Guard.ArgumentNull(registrator, nameof(registrator));

            var config = registrator(new ValidatorsConfigBuilder<TModel>());
            var key = CreateKey(config.RulesetName, typeof(TModel).FullName);
            if (_validatorsConfigContainer.ContainsKey(key))
            {
                _validatorsConfigContainer.Remove(key);
            }

            _validatorsConfigContainer.Add(key, config);
            return this;
        }

        public IValidationModel<TModel> ResolveModel<TModel>(string rulesetName = Constants.DefaultRulestName)
            where TModel : class
        {
            var key = CreateKey(rulesetName, typeof(TModel).FullName);
            if (!_validatorsConfigContainer.ContainsKey(key))
            {
                throw new ArgumentOutOfRangeException(nameof(rulesetName), "Ruleset doesn't exist");
            }

            var config = (ValidatorsConfig<TModel>)_validatorsConfigContainer[key];
            return new GenericValidationModel<TModel>(
                config.ValidatorExecutorsConfig.ExecutorFactory.Create(config.ValidatorExecutorsConfig.ExecutorType),
                config.ValidationModelConfig);
        }

        private Tuple<string, string> CreateKey(string rulesetName, string fullTypeName)
        {
            return new Tuple<string, string>(rulesetName, fullTypeName);
        }
    }
}