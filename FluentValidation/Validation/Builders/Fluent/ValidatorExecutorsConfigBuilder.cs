using System;
using FluentValidationFramework.Validation.Configuration;
using FluentValidationFramework.Validation.Configuration.Enums;
using FluentValidationFramework.Validation.Factories;

namespace FluentValidationFramework.Validation.Builders.Fluent
{
    public class ValidatorExecutorsConfigBuilder<TModel> : IValidatorExecutorsConfigBuilder<TModel>
        where TModel : class
    {
        public ValidatorExecutorsConfig<TModel> WithCustomExecuter(IValidatorExecutor<TModel> executor)
        {
            return new ValidatorExecutorsConfig<TModel>(new ValidatorExecutorFactory<TModel>(() => executor), ValidatorExecutorTypes.Custom);
        }

        public ValidatorExecutorsConfig<TModel> WithExecutorType(ExecutorTypes executorType)
        {
            return new ValidatorExecutorsConfig<TModel>(new ValidatorExecutorFactory<TModel>(), ExecutorTypeMapper.Map(executorType));
        }
    }
}
