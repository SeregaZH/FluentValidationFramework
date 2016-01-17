using FluentValidation.Validation.Models;
using System;

namespace FluentValidation.Validation.Builders.Fluent
{
    public sealed class ValueValidatorDescriptorBuilder : BaseValidatorDescriptorBuilder<ValueValidatorDescriptorBuilder, Func<PropertyName, ValidationValue, string>>
    {
    }
}
