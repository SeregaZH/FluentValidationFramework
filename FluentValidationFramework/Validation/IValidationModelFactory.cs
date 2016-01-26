using FluentValidationFramework.Validation;
using FluentValidationFramework.Validation.Configuration;
using FluentValidationFramework.Validation.Fluent;
using System;

namespace FluentValidationFramework.Validation
{
    public interface IValidationModelFactory
    {
        IValidationModelFactory RegisterConfig<TModel>(Func<IValidatorsConfigBuilder<TModel>, ValidatorsConfig<TModel>> registrator)
            where TModel: class;

        IValidationModel<TModel> ResolveModel<TModel>(string rulesetName = Constants.DefaultRulestName)
            where TModel : class;
    }
}
