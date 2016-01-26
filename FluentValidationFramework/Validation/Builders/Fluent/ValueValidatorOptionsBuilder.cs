using System;
using System.Collections.Generic;
using FluentValidationFramework.Validation.Models.Options;
using FluentValidationFramework.Helpers;

namespace FluentValidationFramework.Validation.Builders.Fluent
{
    public sealed class ValueValidatorOptionsBuilder<TType> : IValueValidatorOptionsBuilder<TType>
    {
        private IEqualityComparer<TType> _comparer;
        private HashSet<TType> _values;

        public IValueValidatorOptions<TType> Build()
        {
            return new ValueValidatorOptions<TType>(
                _values ?? new HashSet<TType>(),
                _comparer ?? EqualityComparer<TType>.Default);
        }

        public IValueValidatorOptionsBuilder<TType> WithComparer(IEqualityComparer<TType> comparer)
        {
            Guard.ArgumentNull(comparer, nameof(comparer));
            _comparer = comparer;
            return this;
        }

        public IValueValidatorOptionsBuilder<TType> WithValues(IEnumerable<TType> values)
        {
            Guard.ArgumentNull(values, nameof(values));
            _values = new HashSet<TType>(values);
            return this;
        }

        public IValueValidatorOptionsBuilder<TType> WithValues(HashSet<TType> values)
        {
            Guard.ArgumentNull(values, nameof(values));
            _values = values;
            return this;
        }
    }
}
