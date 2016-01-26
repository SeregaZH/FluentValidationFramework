using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidationFramework.Validation.Models.Results
{
    [Serializable]
    public sealed class AggregateValidationResult : IValidationResult
    {
        private readonly IEnumerable<ValidationResult> _validationResults;

        public AggregateValidationResult(IEnumerable<ValidationResult> validationResults)
        {
            _validationResults = validationResults;
            FailedValidators = _validationResults.Where(x => !x.IsValid());
        }

        public AggregateValidationResult(IEnumerable<ValidationResult> validationResults, params IEnumerable<ValidationResult>[] additionalValidationResults)
        {
            _validationResults = validationResults
                .Union(additionalValidationResults
                .SelectMany(x => x));
            FailedValidators = _validationResults.Where(x => !x.IsValid());
        }

        public bool IsValid()
        {
            return !FailedValidators.Any();
        }

        public IEnumerable<ValidationResult> FailedValidators { get; }
    }
}
