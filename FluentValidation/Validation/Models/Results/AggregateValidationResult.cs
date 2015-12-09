using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidation.Validation.Models.Results
{
    [Serializable]
    public sealed class AggregateValidationResult : IValidationResult
    {
        private readonly IEnumerable<ValidationResult> _rules;

        public AggregateValidationResult(IEnumerable<ValidationResult> rules)
        {
            _rules = rules;
            FailedRules = _rules.Where(x => !x.IsValid());
        }

        public bool IsValid()
        {
            return !FailedRules.Any();
        }

        public IEnumerable<ValidationResult> FailedRules { get; }
    }
}
