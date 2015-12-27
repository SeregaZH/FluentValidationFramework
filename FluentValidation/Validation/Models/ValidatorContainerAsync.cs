namespace FluentValidation.Validation.Models
{
    public class ValidatorContainerAsync<TModel> : ValidatorContainerBase
    {
        public ValidatorContainerAsync(IValidatorAsync<TModel> validator, long priority)
            : base(priority)
        {
            Validator = validator;
        }

        public IValidatorAsync<TModel> Validator { get; private set; }
    }
}
