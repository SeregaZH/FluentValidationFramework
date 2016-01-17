using System;
using LazyPropValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, string>>;
using LazyValValidationDescriptor = FluentValidation.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidation.Validation.Models.PropertyName, FluentValidation.Validation.Models.ValidationValue, string>>;

namespace FluentValidation.Validation.Builders
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
    }
}
