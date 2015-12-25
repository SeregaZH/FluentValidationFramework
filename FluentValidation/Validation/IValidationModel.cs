﻿using FluentValidation.Validation.Models.Results;
using System.Threading.Tasks;

namespace FluentValidation.Validation
{
    public interface IValidationModel<in TModel>
        where TModel: class
    {
        AggregateValidationResult Validate(TModel model);

        Task<AggregateValidationResult> ValidateAsync(TModel model);
    }
}
