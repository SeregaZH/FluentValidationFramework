using System.Collections.Generic;

namespace FluentValidation.Validation.Models.Options
{
    public sealed class ValueValidatorOptions<TType> : IValueValidatorOptions<TType>
    {
        public ValueValidatorOptions()
        {
            Values = new HashSet<TType>();
            Comparer = EqualityComparer<TType>.Default;
        }

        public ValueValidatorOptions(
            HashSet<TType> values)
        {
            Values = values;
            Comparer = EqualityComparer<TType>.Default;
        }

        public ValueValidatorOptions(
            HashSet<TType> values,
            IEqualityComparer<TType> comparer)
        {
            Comparer = comparer;
            Values = values;
        }

        public IEqualityComparer<TType> Comparer { get; }

        public HashSet<TType> Values { get; }
    }
}
