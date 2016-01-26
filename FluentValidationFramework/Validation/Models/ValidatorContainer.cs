namespace FluentValidationFramework.Validation.Models
{
    public class ValidatorContainer<TModel>
    {
        public ValidatorContainer(IValidator<TModel> validator, long priority)
        {
            Validator = validator;
            Priority = priority;
        }

        public long Priority { get; private set; }

        public IValidator<TModel> Validator { get; private set; }
    }
}
