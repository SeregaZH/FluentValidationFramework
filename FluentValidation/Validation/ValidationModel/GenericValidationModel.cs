using System;
using System.Threading.Tasks;
using FluentValidationFramework.Validation.Models.Results;
using FluentValidationFramework.Helpers;
using FluentValidationFramework.Validation.Configuration;

namespace FluentValidationFramework.Validation.ValidationModel
{
    public class GenericValidationModel<TModel> : IValidationModel<TModel>
        where TModel: class
    {
        private readonly IValidatorExecutor<TModel> _validatorExecutor;
        private readonly ValidationModelConfig<TModel> _configuration;

        public GenericValidationModel(
            IValidatorExecutor<TModel> validatorExecutor,
            ValidationModelConfig<TModel> configuration)
        {
            _validatorExecutor = validatorExecutor;
            _configuration = configuration;
        }

        public AggregateValidationResult Validate(TModel model)
        {
            Guard.ArgumentNull(model, nameof(model));
            var syncValidatorResults = _validatorExecutor.Execute(model, _configuration.Validators);
            return new AggregateValidationResult(syncValidatorResults);
        }

        public async Task<AggregateValidationResult> ValidateAsync(TModel model)
        {
            Guard.ArgumentNull(model, nameof(model));
            var asyncValidatorResults = await _validatorExecutor.ExecuteAsync(model, _configuration.Validators);
            return new AggregateValidationResult(asyncValidatorResults);
        }
    }
}
