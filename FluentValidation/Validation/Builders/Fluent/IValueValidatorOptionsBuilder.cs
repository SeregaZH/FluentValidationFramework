using FluentValidation.Validation.Models.Options;
using System.Collections.Generic;

namespace FluentValidation.Validation.Builders.Fluent
{
    public interface IValueValidatorOptionsBuilder<TType>
    {
        IValueValidatorOptionsBuilder<TType> WithComparer(IEqualityComparer<TType> comparer);

        IValueValidatorOptionsBuilder<TType> WithValues(HashSet<TType> values);

        IValueValidatorOptionsBuilder<TType> WithValues(IEnumerable<TType> values);

        IValueValidatorOptions<TType> Build();
    }
}
