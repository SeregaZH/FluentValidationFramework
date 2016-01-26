using FluentValidationFramework.Validation.Models;
using System;

namespace FluentValidationFramework.Validation.Builders.Fluent
{
    public sealed class ValueValidatorDescriptorBuilder : BaseValidatorDescriptorBuilder<ValueValidatorDescriptorBuilder, Func<PropertyName, ValidationValue, string>>
    {
    }
}
