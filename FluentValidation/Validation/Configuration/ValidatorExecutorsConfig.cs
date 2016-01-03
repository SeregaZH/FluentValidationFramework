using FluentValidation.Validation.Executors;

namespace FluentValidation.Validation.Configuration
{
    public sealed class ValidatorExecutorsConfig<TModel>
        where TModel: class
    {
        public ValidatorExecutorsConfig(
            IValidatorExecutor<TModel> executor,
            IValidatorExecutorAsync<TModel> executorAsync)
        {
            Executor = executor;
            ExecutorAsync = executorAsync;
        }

        public ValidatorExecutorsConfig()
        {
            Executor = new ValidatorExecutor<TModel>();
            ExecutorAsync = new ValidatorExecutorAsync<TModel>();
        }

        public IValidatorExecutor<TModel> Executor { get; private set; }

        public IValidatorExecutorAsync<TModel> ExecutorAsync { get; private set; }
    }
}
