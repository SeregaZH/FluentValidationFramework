using System;
using FluentValidation.Validation.Models;

namespace FluentValidation.UnitTests.Validators
{
    public static class ValidatorTestHelper
    {
        public static ValidatorDescriptor CreateDefaultValidationDescriptor()
        {
            return CreateDefaultValidationDescriptor(Guid.NewGuid());
        }

        public static ValidatorDescriptor CreateDefaultValidationDescriptor(Guid validatorId)
        {
            return new ValidatorDescriptor(validatorId, "TestRule", "Error", "Descriptor");
        }
    }
}
