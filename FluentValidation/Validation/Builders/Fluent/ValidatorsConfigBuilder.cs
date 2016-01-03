using System;
using FluentValidation.Validation.Configuration;
using FluentValidation.Helpers;
using FluentValidation.Validation.Fluent;

namespace FluentValidation.Validation.Builders.Fluent
{
    public class ValidatorsConfigBuilder<TModel> : IValidatorsConfigBuilder<TModel>
        where TModel : class
    {
        private Func<IValidationModelConfigBuilder<TModel>, ValidationModelConfig<TModel>> _validationConfigModelBuilder;
        private Func<IValidatorExecutorsConfigBuilder<TModel>, ValidatorExecutorsConfig<TModel>> _validatorExecutorsConfigBuilder;

        public ValidatorsConfig<TModel> Build(string rulesetName = null)
        {
            var validatorConfigModel = _validationConfigModelBuilder(new ValidationModelConfigBuilder<TModel>());
            var validatorExecutorsConfig = _validatorExecutorsConfigBuilder(new ValidatorExecutorsConfigBuilder<TModel>());
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
