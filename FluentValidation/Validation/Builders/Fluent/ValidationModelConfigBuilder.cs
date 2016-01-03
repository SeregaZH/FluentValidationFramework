using FluentValidation.Validation.Configuration;
using FluentValidation.Validation.Fluent;
using FluentValidation.Validation.Models;
using System.Collections.Generic;

namespace FluentValidation.Validation.Builders.Fluent
{
    public class ValidationModelConfigBuilder<TModel> : IValidationModelConfigBuilder<TModel>
    {
        private readonly List<ValidatorContainerAsync<TModel>> _asyncValidatorContainers;
        private readonly List<ValidatorContainer<TModel>> _validatorContainers;

        public ValidationModelConfigBuilder()
        {
            _asyncValidatorContainers = new List<ValidatorContainerAsync<TModel>>();
            _validatorContainers = new List<ValidatorContainer<TModel>>();
        }

        public IValidationModelConfigBuilder<TModel> AddAsyncValidator(ValidatorContainerAsync<TModel> asyncValidatorContainer)
        {
            _asyncValidatorContainers.Add(asyncValidatorContainer);
            return this;
        }

        public IValidationModelConfigBuilder<TModel> AddValidator(ValidatorContainer<TModel> validatorContainer)
        {
            _validatorContainers.Add(validatorContainer);
            return this;
        }

        public ValidationModelConfig<TModel> Build(TModel model)
        {
            return new ValidationModelConfig<TModel>(_validatorContainers, _asyncValidatorContainers);
        }
    }
}
