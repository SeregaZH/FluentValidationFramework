namespace FluentValidation.Validation.Models
{
    public class ValidatorContainer<TModel> : ValidatorContainerBase
    {
        public ValidatorContainer(IValidator<TModel> validator, long priority)
            : base(priority)
        {
            Validator = validator;            
        }

        public IValidator<TModel> Validator { get; private set; }
    }
}
