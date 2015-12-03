using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;

namespace FluentValidation.Validation.Validators
{
    public sealed class StringInvalidValuesValidator<TModel> : InvalidValuesValidator<TModel, string>
    {
        public StringInvalidValuesValidator(ValidatorDescriptor descriptor, int priority, Expression<Func<TModel, string>> propertyGetter, HashSet<string> invalidValues, StringInvalidValuesValidatorOptions options) 
            : base(descriptor, priority, propertyGetter, new HashSet<string>(invalidValues.Select(x => options.IsTrimmed ? x?.Trim() :x ), options.Comparer))
        {
            
        }
    }
}
