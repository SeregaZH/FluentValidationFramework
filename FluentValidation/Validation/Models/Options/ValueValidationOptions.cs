using System.Collections.Generic;

namespace FluentValidation.Validation.Models.Options
{
    public sealed class ValueValidationOptions<TType> : IValueValidatorOptions<TType>
    {
        public ValueValidationOptions()
        {
            Values = new HashSet<TType>();
            Comparer = EqualityComparer<TType>.Default;
        }

        public ValueValidationOptions(
            HashSet<TType> values)
        {
            Values = values;
            Comparer = EqualityComparer<TType>.Default;
        }

        public ValueValidationOptions(
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
