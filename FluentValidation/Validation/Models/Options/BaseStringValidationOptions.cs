namespace FluentValidation.Validation.Models.Options
{
    public abstract class BaseStringValidationOptions
    {
        protected BaseStringValidationOptions(bool isTrimmed)
        {
            IsTrimmed = isTrimmed;
        }

        public bool IsTrimmed { get; }
    }
}
