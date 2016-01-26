using System;
using LazyPropValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, string>>;
using LazyValValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, FluentValidationFramework.Validation.Models.ValidationValue, string>>;

namespace FluentValidationFramework.Validation.Builders
{
    internal static class DefaultValidatorDescriptorFactory
    {
        private const string RuleKeyPrefix = "Key";

        internal static LazyPropValidationDescriptor PropertyRequired =>
            new LazyPropValidationDescriptor(
                Guid.NewGuid(),
                $"{ RuleKeyPrefix }:PropertyRequired",
                propertyName => $"{ propertyName.Name } Required",
                propertyName => $"The { propertyName.Name } is required");

        internal static LazyPropValidationDescriptor CollectionPropertyRequired =>
            new LazyPropValidationDescriptor(
                Guid.NewGuid(),
                $"{ RuleKeyPrefix }:CollectionRequired",
                propertyName => $"Collection { propertyName.Name } required",
                propertyName => $"The { propertyName.Name } collection is required");

        internal static LazyValValidationDescriptor DeniedValues =>
            new LazyValValidationDescriptor(
                Guid.NewGuid(),
                $"{ RuleKeyPrefix }:DeniedValue",
                (propName, propValue) => $"The value: { propValue.Value } is denied for property: { propName.Name }",
                (propName, propValue) => $"The value: { propValue.Value } is denied for property: { propName.Name }");

        internal static LazyValValidationDescriptor DeniedStringValues =>
            new LazyValValidationDescriptor(
                Guid.NewGuid(),
                $"{ RuleKeyPrefix }:DeniedStringValue",
                (propName, propValue) => $"The string value: { propValue.Value } is denied for property: { propName.Name }",
                (propName, propValue) => $"The string value: { propValue.Value } is denied for property: { propName.Name }");
    }
}
