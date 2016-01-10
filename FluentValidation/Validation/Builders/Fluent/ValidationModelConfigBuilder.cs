using FluentValidation.Validation.Configuration;
using FluentValidation.Validation.Fluent;
using FluentValidation.Validation.Models;
using System.Collections.Generic;

namespace FluentValidation.Validation.Builders.Fluent
{
    public class ValidationModelConfigBuilder<TModel> : IValidationModelConfigBuilder<TModel>
    {
        private readonly List<ValidatorContainer<TModel>> _validatorContainers;

        public ValidationModelConfigBuilder()
        {
            _validatorContainers = new List<ValidatorContainer<TModel>>();
        }

        public IValidationModelConfigBuilder<TModel> AddValidator(ValidatorContainer<TModel> validatorContainer)
        {
            _validatorContainers.Add(validatorContainer);
            return this;
        }

        public ValidationModelConfig<TModel> Build()
        {
            return new ValidationModelConfig<TModel>(_validatorContainers);
        }
    }
}
