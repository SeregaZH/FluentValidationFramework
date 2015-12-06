using System.Collections.Generic;

namespace FluentValidation.Validation.Models.Options
{
    public interface IValueValidatorOptions<TType>
    {
        IEqualityComparer<TType> Comparer { get; }

        HashSet<TType> Values { get; }
    }
}
