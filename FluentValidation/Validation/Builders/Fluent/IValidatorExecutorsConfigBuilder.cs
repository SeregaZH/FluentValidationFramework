using FluentValidation.Validation.Configuration;
using FluentValidation.Validation.Configuration.Enums;

namespace FluentValidation.Validation.Builders.Fluent
{
    public interface IValidatorExecutorsConfigBuilder<TModel>
        where TModel : class
    {
        ValidatorExecutorsConfig<TModel> WithCustomExecuter(IValidatorExecutor<TModel> executor);

        ValidatorExecutorsConfig<TModel> WithExecutorType(ExecutorTypes executorType);
    }
}
