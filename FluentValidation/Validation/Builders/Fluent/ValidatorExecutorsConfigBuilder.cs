using System;
using FluentValidation.Validation.Configuration;
using FluentValidation.Validation.Configuration.Enums;
using FluentValidation.Validation.Factories;

namespace FluentValidation.Validation.Builders.Fluent
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
