using FluentValidationFramework.Validation.Configuration.Enums;
using FluentValidationFramework.Validation.Factories;

namespace FluentValidationFramework.Validation.Configuration
{
    public sealed class ValidatorExecutorsConfig<TModel>
        where TModel: class
    {
        public ValidatorExecutorsConfig(
            ValidatorExecutorFactory<TModel> executorFactory,
            ValidatorExecutorTypes executorType)
        {
            ExecutorFactory = executorFactory;
            ExecutorType = executorType;
        }

        public ValidatorExecutorsConfig()
        {
            ExecutorFactory = new ValidatorExecutorFactory<TModel>();
            ExecutorType = ValidatorExecutorTypes.Plain;
        }

        public IValidatorExecutorFactory<TModel> ExecutorFactory { get; private set; }

        public ValidatorExecutorTypes ExecutorType { get; private set; }
    }
}
