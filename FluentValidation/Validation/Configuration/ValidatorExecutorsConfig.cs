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

        public IValidatorExecutor<TModel> Executor { get; private set; }

        public IValidatorExecutorAsync<TModel> ExecutorAsync { get; private set; }
    }
}
