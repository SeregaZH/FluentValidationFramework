using FluentValidation.Validation.Configuration.Enums;

namespace FluentValidation.Validation
{
    public interface IValidatorExecutorFactory<TModel>
        where TModel: class
    {
        IValidatorExecutor<TModel> Create(ValidatorExecutorTypes type);
    }
}
