using FluentValidationFramework.UnitTests;
using FluentValidationFramework.UnitTests.Validators;
using FluentValidationFramework.Validation;
using FluentValidationFramework.Validation.Models;
using FluentValidationFramework.Validation.Models.Results;
using Moq;
using System;
using System.Threading.Tasks;

namespace FluentValidationFramework.Tests.Executors
{
    public static class ValidatorExecutorTestHelper
    {
        public static ValidatorContainer<FakeValidationTargetModel> CreateValidatorContainer(IValidator<FakeValidationTargetModel> validator, long priority)
        {
            return new ValidatorContainer<FakeValidationTargetModel>(validator, priority);
        }

        public static Mock<IValidator<FakeValidationTargetModel>> CreateMockValidator(bool isValid, Guid id)
        {
            var mockValidator = new Mock<IValidator<FakeValidationTargetModel>>();
            mockValidator.Setup(x => x.Validate(It.IsAny<FakeValidationTargetModel>()))
                .Returns(new ValidationResult(isValid, ValidatorTestHelper.CreateDefaultValidationDescriptor(id)));
            mockValidator.Setup(x => x.ValidateAsync(It.IsAny<FakeValidationTargetModel>()))
                .Returns(Task.FromResult(new ValidationResult(isValid, ValidatorTestHelper.CreateDefaultValidationDescriptor(id))));
            return mockValidator;
        }
    }
}
