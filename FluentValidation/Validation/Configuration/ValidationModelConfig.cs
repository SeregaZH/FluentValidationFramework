using FluentValidation.Validation.Models;
using System.Collections.Generic;

namespace FluentValidation.Validation.Configuration
{
    public sealed class ValidationModelConfig<TModel>
    {
        public ValidationModelConfig(
            IEnumerable<ValidatorContainer<TModel>> validators,
            IEnumerable<ValidatorContainerAsync<TModel>> asyncValidators)
        {
            Validators = validators;
            AsyncValidators = asyncValidators;
        } 

        public IEnumerable<ValidatorContainer<TModel>> Validators { get; private set; }

        public IEnumerable<ValidatorContainerAsync<TModel>> AsyncValidators { get; private set; }
    }
}
