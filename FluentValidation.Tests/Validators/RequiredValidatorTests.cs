using FluentValidation.UnitTests;
using FluentValidation.UnitTests.Validators;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace FluentValidation.Tests.Validators
{
    [TestClass]
    public class RequiredValidatorTests
    {
        [TestMethod]
        public void TestRequiredValidatorForNullPropertyShouldBeInvalid()
        {
            RequiredValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel(),
                propertyToValidate: x => x.ReferenceTypeValidationTargetProperty,
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.ReferenceTypeValidationTargetProperty));
                });
        }

        [TestMethod]
        public void TestRequiredValidatorForNotNullPropertyShouldBeValid()
        {
            RequiredValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { ReferenceTypeValidationTargetProperty = new object() },
                propertyToValidate: x => x.ReferenceTypeValidationTargetProperty,
                asserts: result =>
                {
                    Assert.IsTrue(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.ReferenceTypeValidationTargetProperty));
                });
        }

        private void RequiredValidatorTestTemplate<TModel, TValue>(
            Func<TModel> validationTargetFactory,
            Expression<Func<TModel, TValue>> propertyToValidate,
            Action<ValidationResult> asserts)
            where TModel : FakeValidationTargetModel
        {
            var validationTarget = validationTargetFactory();
            var validatorDescriptor = ValidatorTestHelper.CreateDefaultLazyPropertyValidationDescriptor();
            var targetValidator = new RequiredValidator<TModel, TValue>(validatorDescriptor, propertyToValidate);

            ValidationResult result = targetValidator.Validate(validationTarget);

            asserts(result);
        }
    }
}
