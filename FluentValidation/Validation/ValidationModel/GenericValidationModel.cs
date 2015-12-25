using System;
using System.Threading.Tasks;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Helpers;
using FluentValidation.Validation.Configuration;

namespace FluentValidation.Validation.ValidationModel
{
    public class GenericValidationModel<TModel> : IValidationModel<TModel>
        where TModel: class
    {
        private readonly IValidatorExecutor<TModel> _validatorExecutor;
        private readonly IValidatorExecutorAsync<TModel> _asyncValidatorExecutor;
        private readonly ValidationModelConfig<TModel> _configuration;

        public GenericValidationModel(
            IValidatorExecutor<TModel> validatorExecutor,
            IValidatorExecutorAsync<TModel> asyncValidatorExecutor,
            ValidationModelConfig<TModel> configuration)
        {
            _validatorExecutor = validatorExecutor;
            _asyncValidatorExecutor = asyncValidatorExecutor;
            _configuration = configuration;
        }

        public AggregateValidationResult Validate(TModel model)
        {
            Guard.ArgumentNull(model, nameof(model));
            var syncValidatorResults = _validatorExecutor.Execute(model, _configuration.Validators);
            var asyncValidatorResults = _asyncValidatorExecutor.ExecuteAsync(model, _configuration.AsyncValidators).Result;
            return new AggregateValidationResult(syncValidatorResults, asyncValidatorResults);
        }

        public async Task<AggregateValidationResult> ValidateAsync(TModel model)
        {
            Guard.ArgumentNull(model, nameof(model));
            var syncValidatorResults = _validatorExecutor.Execute(model, _configuration.Validators);
            var asyncValidatorResults = await _asyncValidatorExecutor.ExecuteAsync(model, _configuration.AsyncValidators);
            return new AggregateValidationResult(syncValidatorResults, asyncValidatorResults);
        }
    }
}
