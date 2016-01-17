using System;
using System.Collections.Generic;
using FluentValidation.Validation.Models.Options;

namespace FluentValidation.Validation.Builders.Fluent
{
    public sealed class ValueValidatorOptionsBuilder<TType> : IValueValidatorOptionsBuilder<TType>
    {
        public IValueValidatorOptions<TType> Build()
        {
            throw new NotImplementedException();
        }

        public IValueValidatorOptionsBuilder<TType> WithComparer(IEqualityComparer<TType> comparer)
        {
            throw new NotImplementedException();
        }

        public IValueValidatorOptionsBuilder<TType> WithValues(HashSet<TType> values)
        {
            throw new NotImplementedException();
        }
    }
}
