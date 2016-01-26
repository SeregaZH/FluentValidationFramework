using FluentValidationFramework.Validation.Configuration.Enums;

namespace FluentValidationFramework.Validation
{
    public interface IValidatorExecutorFactory<TModel>
        where TModel: class
    {
        IValidatorExecutor<TModel> Create(ValidatorExecutorTypes type);
    }
}
