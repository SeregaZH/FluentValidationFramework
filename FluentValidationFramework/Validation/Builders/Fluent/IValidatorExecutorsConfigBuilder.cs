using FluentValidationFramework.Validation.Configuration;
using FluentValidationFramework.Validation.Configuration.Enums;

namespace FluentValidationFramework.Validation.Builders.Fluent
{
    public interface IValidatorExecutorsConfigBuilder<TModel>
        where TModel : class
    {
        ValidatorExecutorsConfig<TModel> WithCustomExecuter(IValidatorExecutor<TModel> executor);

        ValidatorExecutorsConfig<TModel> WithExecutorType(ExecutorTypes executorType);
    }
}
