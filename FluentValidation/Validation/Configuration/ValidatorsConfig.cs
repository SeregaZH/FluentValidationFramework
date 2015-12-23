using System;
using System.Collections.Generic;

namespace FluentValidation.Validation.Configuration
{
    public class ValidatorsConfig<TModel>
    {
        public ValidatorsConfig(string rulesetName, IEnumerable<IValidator<TModel>> validators)
        {
            RulesetName = rulesetName;
            Validators = validators;
        }

        public string RulesetName { get; private set; }

        public Type TargetType { get { return typeof(TModel); } }

        public IEnumerable<IValidator<TModel>> Validators { get; private set; }

        public IEnumerable<IValidatorAsync<TModel>> AsyncValidators { get; private set; }
    }
}
