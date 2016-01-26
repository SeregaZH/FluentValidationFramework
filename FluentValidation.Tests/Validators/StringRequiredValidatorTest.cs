using System;
using System.Linq.Expressions;
using FluentValidationFramework.Validation.Models;
using FluentValidationFramework.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidationFramework.Validation.Models.Options;
using FluentValidationFramework.Validation.Models.Results;

namespace FluentValidationFramework.UnitTests.Validators
{
    [TestClass]
    public sealed class StringRequiredValidatorTest
    {
        [TestMethod]
        public void TestStringRequiredValidatorForNullStringShouldBeFailed()
        {
            RequiredStringValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel(),
                propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new BaseStringValidationOptions(true),
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.StringValidationTargetProperty));
                });
        }

        [TestMethod]
        public void TestStringRequiredValidatorForEmptyStringShouldBeFailed()
        {
            RequiredStringValidatorTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = string.Empty },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new BaseStringValidationOptions(true),
               asserts: result =>
               {
                   Assert.IsFalse(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestStringRequiredValidatorForSpaceStringShouldBeFailedForTrimmedString()
        {
            RequiredStringValidatorTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "        " },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new BaseStringValidationOptions(true),
               asserts: result =>
               {
                   Assert.IsFalse(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestStringRequiredValidatorForSpaceStringShouldBeSucceedForNoneTrimmedString()
        {
            RequiredStringValidatorTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "        " },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new BaseStringValidationOptions(false),
               asserts: result =>
               {
                   Assert.IsTrue(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }


        private void RequiredStringValidatorTestTemplate<TModel>(
            Func<TModel> validationTargetFactory,
            Expression<Func<TModel, string>> propertyToValidate,
            Func<BaseStringValidationOptions> optionsFactory,
            Action<ValidationResult> asserts) 
            where TModel: FakeValidationTargetModel
        {
            var validationTarget = validationTargetFactory();
            var ruleDescriptor = ValidatorTestHelper.CreateDefaultLazyPropertyValidationDescriptor();
            var targetRule = new StringRequiredValidator<TModel>(ruleDescriptor, propertyToValidate, optionsFactory());

            ValidationResult result = targetRule.Validate(validationTarget);

            asserts(result);
        }
    }
}
