using FluentValidation.Validation.Models;
using System;

namespace FluentValidation.Validation.Builders
{
    internal static class DefaultValidatorDescriptorFactory
    {
        private const string RuleKeyPrefix = "Key";

        internal static ValidatorDescriptor Required =>
            new ValidatorDescriptor(
                Guid.NewGuid(),
                $"{ RuleKeyPrefix }:Required",
                "Required",
                "Entity is required");

        internal static Func<string, ValidatorDescriptor> CollectionPropertyRequired =>
            (propertyName) => new ValidatorDescriptor(
                Guid.NewGuid(),
                $"{ RuleKeyPrefix }:CollectionRequired",
                $"Collection { propertyName } required",
                $"The { propertyName } collection is required");

        internal static Func<string, ValidatorDescriptor> PropertyRequired =>
            (propertyName) => new ValidatorDescriptor(
                Guid.NewGuid(),
                $"{ RuleKeyPrefix }:PropertyRequired",
                $"{ propertyName } Required",
                $"The { propertyName } is required");
    }
}
