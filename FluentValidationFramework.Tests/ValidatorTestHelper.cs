using System;
using FluentValidationFramework.Validation.Models;
using LazyPropValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, string>>;
using LazyValValidationDescriptor = FluentValidationFramework.Validation.Models.BaseLazyValidatorDescriptor<System.Func<FluentValidationFramework.Validation.Models.PropertyName, FluentValidationFramework.Validation.Models.ValidationValue, string>>;

namespace FluentValidationFramework.UnitTests.Validators
{
    public static class ValidatorTestHelper
    {
        public static LazyPropValidationDescriptor CreateDefaultLazyPropertyValidationDescriptor()
        {
            return CreateDefaultLazyPropertyValidationDescriptor(Guid.NewGuid());
        }

        public static LazyPropValidationDescriptor CreateDefaultLazyPropertyValidationDescriptor(Guid validatorId)
        {
            return new LazyPropValidationDescriptor(validatorId, "TestRule", p => "Error", p => "Descriptor");
        }

        public static ValidatorDescriptor CreateDefaultValidationDescriptor(Guid validatorId)
        {
            return new ValidatorDescriptor(validatorId, "TestRule", "Error", "Descriptor");
        }

        public static ValidatorDescriptor CreateDefaultValidationDescriptor()
        {
            return CreateDefaultValidationDescriptor(Guid.NewGuid());
        }

        public static LazyValValidationDescriptor CreateDefaultLazyValueValidatorDescriptor(Guid validatorId)
        {
            return new LazyValValidationDescriptor(validatorId, "TestRule", (p, v) => "Error", (p, v) => "Descriptor");
        }

        public static LazyValValidationDescriptor CreateDefaultLazyValueValidatorDescriptor()
        {
            return CreateDefaultLazyValueValidatorDescriptor(Guid.NewGuid());
        }
    }
}
