using FluentValidation.Validation.Configuration;

namespace FluentValidation.Validation.Builders.Fluent
{
    public class ValidatorExecutorsConfigBuilder<TModel> : IValidatorExecutorsConfigBuilder<TModel>
        where TModel : class
    {
        private IValidatorExecutor<TModel> _executor;
        private IValidatorExecutorAsync<TModel> _executorAsync;

        public ValidatorExecutorsConfig<TModel> Build()
        {
            return new ValidatorExecutorsConfig<TModel>(_executor, _executorAsync);
        }

        public IValidatorExecutorsConfigBuilder<TModel> WithAsyncExecuter(IValidatorExecutorAsync<TModel> asyncExecutor)
        {
            _executorAsync = asyncExecutor;
            return this;
        }

        public IValidatorExecutorsConfigBuilder<TModel> WithExecuter(IValidatorExecutor<TModel> executor)
        {
            _executor = executor;
            return this;
        }
    }
}
