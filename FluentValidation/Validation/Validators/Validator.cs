using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation.Validators
{
    public abstract class Validator<TModel> : IValidator<TModel>
    {
        protected Validator(ValidatorDescriptor descriptor, int priority)
        {
            Descriptor = descriptor;
            Priority = priority;
        }

        protected abstract ValidationResult ValidateModel(TModel model);

        public ValidatorDescriptor Descriptor { get; private set; }

        public ValidationResult Validate(TModel model)
        {
            return ValidateModel(model);
        }

        public int Priority { get; private set; }
    }
}
