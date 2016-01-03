using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Validation.Models;

namespace FluentValidation.UnitTests.Validators
{
    public static class ValidatorTestHelper
    {
        public static ValidatorDescriptor CreateDefaultValidationDescriptor()
        {
            return new ValidatorDescriptor(Guid.NewGuid(), "TestRule", "Error", "Descriptor");
        }
    }
}
