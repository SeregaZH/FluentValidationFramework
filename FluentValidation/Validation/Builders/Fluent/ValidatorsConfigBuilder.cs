using System;
using FluentValidation.Validation.Configuration;
using FluentValidation.Helpers;
using FluentValidation.Validation.Fluent;
using System.Collections.Generic;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Builders.Fluent
{
    public class ValidatorsConfigBuilder<TModel> : IValidatorsConfigBuilder<TModel>
        where TModel : class
    {
        private Func<IValidationModelConfigBuilder<TModel>, ValidationModelConfig<TModel>> _validationConfigModelBuilder;
        private Func<IValidatorExecutorsConfigBuilder<TModel>, ValidatorExecutorsConfig<TModel>> _validatorExecutorsConfigBuilder;

        public ValidatorsConfig<TModel> Build(string rulesetName = Constants.DefaultRulestName)
        {
            var validatorConfigModel = _validationConfigModelBuilder != null
                ? _validationConfigModelBuilder(new ValidationModelConfigBuilder<TModel>())
                : new ValidationModelConfig<TModel>();

            var validatorExecutorsConfig = _validatorExecutorsConfigBuilder != null
                ? _validatorExecutorsConfigBuilder(new ValidatorExecutorsConfigBuilder<TModel>())
                : new ValidatorExecutorsConfig<TModel>();

            return new ValidatorsConfig<TModel>(rulesetName, validatorConfigModel, validatorExecutorsConfig);
        }

        public IValidatorsConfigBuilder<TModel> WithValidationModel(Func<IValidationModelConfigBuilder<TModel>, ValidationModelConfig<TModel>> configBuilder)
        {
            Guard.ArgumentNull(configBuilder, nameof(configBuilder));
            _validationConfigModelBuilder = configBuilder;
            return this;
        }

        public IValidatorsConfigBuilder<TModel> WithValidatorExecutors(Func<IValidatorExecutorsConfigBuilder<TModel>, ValidatorExecutorsConfig<TModel>> configBuilder)
        {
            Guard.ArgumentNull(configBuilder, nameof(configBuilder));
            _validatorExecutorsConfigBuilder = configBuilder;
            return this;
        }
    }
}
