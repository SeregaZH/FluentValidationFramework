using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using FluentValidation.Validation.Models.Options;
using FluentValidation.Validation.Models.Results;
using FluentValidation.UnitTests.Validators;
using FluentValidation.Validation.Validators;
using FluentValidation.UnitTests;
using FluentValidation.Validation.Models;

namespace FluentValidation.Tests.Validators
{
    [TestClass]
    public class DeniedStringValuesValidatorTests
    {
        [TestMethod]
        public void TestDeniedStringValuesValidatorForNullTrimmedStringShouldBeSucceed()
        {
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel(),
                propertyToValidate: model => model.StringValidationTargetProperty,
                optionsFactory: () => new StringValuesValidatorOptions(true, StringComparer.CurrentCulture, new HashSet<string>()),
                asserts: result => 
                {
                    Assert.IsTrue(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(nameof(FakeValidationTargetModel.StringValidationTargetProperty),
                        ((PropertyValidationResult)result).PropertyName);
                });
        }

        [TestMethod]
        public void TestDeniedStringValuesValidatorForValidTrimmedStringShouldBeSucceed()
        {
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = Guid.NewGuid().ToString() },
                propertyToValidate: model => model.StringValidationTargetProperty,
                optionsFactory: () => new StringValuesValidatorOptions(true, StringComparer.CurrentCulture, new HashSet<string>(new string[] { Guid.NewGuid().ToString() })),
                asserts: result =>
                {
                    Assert.IsTrue(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(nameof(FakeValidationTargetModel.StringValidationTargetProperty),
                        ((PropertyValidationResult)result).PropertyName);
                });
        }

        [TestMethod]
        public void TestDeniedStringValuesValidatorForValidNoneTrimmedStringShouldBeSucceed()
        {
            const string InvalidValue = "Invalid";
            const string WrappedInvalidValue = "  " + InvalidValue + "  ";
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = WrappedInvalidValue },
                propertyToValidate: model => model.StringValidationTargetProperty,
                optionsFactory: () => new StringValuesValidatorOptions(false, StringComparer.CurrentCulture, new HashSet<string>(new string[] { InvalidValue })),
                asserts: result =>
                {
                    Assert.IsTrue(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(nameof(FakeValidationTargetModel.StringValidationTargetProperty),
                        ((PropertyValidationResult)result).PropertyName);
                });
        }

        [TestMethod]
        public void TestDeniedStringValuesValidatorForInvalidTrimmedStringShouldBeFailed()
        {
            const string InvalidValue = "       Invalid        ";
            const string WrappedInvalidValue = "  " + InvalidValue + "  ";
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = InvalidValue },
                propertyToValidate: model => model.StringValidationTargetProperty,
                optionsFactory: () => new StringValuesValidatorOptions(true, StringComparer.CurrentCulture, new HashSet<string>(new string[] { WrappedInvalidValue })),
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(nameof(FakeValidationTargetModel.StringValidationTargetProperty),
                        ((PropertyValidationResult)result).PropertyName);
                });
        }


        [TestMethod]
        public void TestDeniedStringValuesValidatorForInvalidNonTrimmedStringWithInvariantComparedShouldBeFailed()
        {
            const string InvalidValue = "encyclopædia";
            const string ValueToCompare = "encyclopaedia";
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = ValueToCompare },
                propertyToValidate: model => model.StringValidationTargetProperty,
                optionsFactory: () => new StringValuesValidatorOptions(false, StringComparer.InvariantCulture, new HashSet<string>(new string[] { InvalidValue })),
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(nameof(FakeValidationTargetModel.StringValidationTargetProperty),
                        ((PropertyValidationResult)result).PropertyName);
                });
        }

        [TestMethod]
        public void TestDeniedStringValuesValidatorForInvalidNonTrimmedStringWithInvariantComparedCaseIgnoredShouldBeFailed()
        {
            const string InvalidValue = "encyclopædia";
            const string ValueToCompare = "Encyclopaedia";
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = ValueToCompare },
                propertyToValidate: model => model.StringValidationTargetProperty,
                optionsFactory: () => new StringValuesValidatorOptions(false, StringComparer.InvariantCultureIgnoreCase, new HashSet<string>(new string[] { InvalidValue })),
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(nameof(FakeValidationTargetModel.StringValidationTargetProperty),
                        ((PropertyValidationResult)result).PropertyName);
                });
        }

        private void DeniedValuesValidatorTestTemplate<TModel>(
                                        Func<TModel> validationTargetFactory,
                                        Expression<Func<TModel, string>> propertyToValidate,
                                        Func<StringValuesValidatorOptions> optionsFactory,
                                        Action<ValidationResult> asserts)
        {
            var validationTarget = validationTargetFactory();
            var validatorDescriptor = ValidatorTestHelper.CreateDefaultLazyValueValidatorDescriptor();
            var targetValidator = new DeniedStringValuesValidator<TModel>(validatorDescriptor, propertyToValidate, optionsFactory());

            ValidationResult result = targetValidator.Validate(validationTarget);

            asserts(result);
        }
    }
}
