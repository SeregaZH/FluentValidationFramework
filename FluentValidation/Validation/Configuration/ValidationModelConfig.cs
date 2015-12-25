using System.Collections.Generic;

namespace FluentValidation.Validation.Configuration
{
    public class ValidationModelConfig<TModel>
    {
        public ValidationModelConfig(
            IEnumerable<IValidator<TModel>> validators,
            IEnumerable<IValidatorAsync<TModel>> asyncValidators)
        {
            Validators = validators;
            AsyncValidators = asyncValidators;
        } 

        public IEnumerable<IValidator<TModel>> Validators { get; private set; }

        public IEnumerable<IValidatorAsync<TModel>> AsyncValidators { get; private set; }
    }
}
