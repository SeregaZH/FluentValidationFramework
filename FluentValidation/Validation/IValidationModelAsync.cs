using FluentValidation.Validation.Models.Results;
using System.Threading.Tasks;

namespace FluentValidation.Validation
{
    public interface IValidationModelAsync<in TModel>
    {
        Task<AggregateValidationResult> Validate(TModel model);
    }
}
