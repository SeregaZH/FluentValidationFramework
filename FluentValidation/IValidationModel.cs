using FluentValidation.Validation.Models.Results;
using System.Threading.Tasks;

namespace FluentValidation.Validation.ValidationModel
{
    public interface IValidationModel<in TModel>
    {
        AggregateValidationResult Validate(TModel model);

        Task<AggregateValidationResult> ValidateAsync(TModel model);
    }
}
