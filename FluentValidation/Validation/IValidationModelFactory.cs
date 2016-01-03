using FluentValidation.Validation;
using FluentValidation.Validation.Configuration;
using FluentValidation.Validation.Fluent;
using System;

namespace FluentValidation.Validation
{
    public interface IValidationModelFactory
    {
        IValidationModelFactory RegisterConfig<TModel>(Func<IValidatorsConfigBuilder<TModel>, ValidatorsConfig<TModel>> registrator)
            where TModel: class;

        IValidationModel<TModel> ResolveModel<TModel>(string rulesetName = Constants.DefaultRulestName)
            where TModel : class;
    }
}
