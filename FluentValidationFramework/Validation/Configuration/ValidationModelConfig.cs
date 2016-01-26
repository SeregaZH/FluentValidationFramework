using FluentValidationFramework.Validation.Models;
using System.Collections.Generic;

namespace FluentValidationFramework.Validation.Configuration
{
    public sealed class ValidationModelConfig<TModel>
    {
        public ValidationModelConfig(IEnumerable<ValidatorContainer<TModel>> validators)
        {
            Validators = validators;
        }

        public ValidationModelConfig()
        {
            Validators = new List<ValidatorContainer<TModel>>();
        }

        public IEnumerable<ValidatorContainer<TModel>> Validators { get; private set; }
    }
}
