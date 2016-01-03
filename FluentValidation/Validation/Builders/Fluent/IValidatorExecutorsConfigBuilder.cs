using FluentValidation.Validation.Configuration;

namespace FluentValidation.Validation.Builders.Fluent
{
    public interface IValidatorExecutorsConfigBuilder<TModel>
        where TModel : class
    {
        IValidatorExecutorsConfigBuilder<TModel> WithExecuter(IValidatorExecutor<TModel> executor);

        IValidatorExecutorsConfigBuilder<TModel> WithAsyncExecuter(IValidatorExecutorAsync<TModel> asyncExecutor);

        ValidatorExecutorsConfig<TModel> Build();
    }
}
