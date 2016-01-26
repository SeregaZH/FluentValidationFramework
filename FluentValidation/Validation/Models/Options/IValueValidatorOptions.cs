using System.Collections.Generic;

namespace FluentValidationFramework.Validation.Models.Options
{
    public interface IValueValidatorOptions<TType>
    {
        IEqualityComparer<TType> Comparer { get; }

        HashSet<TType> Values { get; }
    }
}
