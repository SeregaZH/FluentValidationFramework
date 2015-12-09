using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Validation
{
    public interface IValidationModel<in TModel>
    {
        AggregateValidationResult Validate(TModel model); 
    }
}
