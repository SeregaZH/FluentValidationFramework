using System;
using System.Linq.Expressions;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Validation.Models.Options;
using FluentValidation.Validation.Models.Results;

namespace FluentValidation.Tests.Validators
{
    [TestClass]
    public sealed class StringRequiredValidatorTest
    {
        [TestMethod]
        public void TestStringRequiredRuleForNullString()
        {
            RequiredStringRuleTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel(),
                propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.InvariantCulture),
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.Equals(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.StringValidationTargetProperty));
                });
        }

        [TestMethod]
        public void TestStringRequiredRuleForEmptyString()
        {
            RequiredStringRuleTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = string.Empty },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.InvariantCulture),
               asserts: result =>
               {
                   Assert.IsFalse(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestStringRequiredRuleForSpaceString()
        {
            RequiredStringRuleTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "        " },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.InvariantCulture),
               asserts: result =>
               {
                   Assert.IsFalse(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestStringRequiredRuleForInvalidNotEmptyStringInvaliantCulture()
        {
            RequiredStringRuleTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "encyclopaedia" },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.InvariantCulture),
               asserts: result =>
               {
                   Assert.IsFalse(result.IsValid()); 
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestStringRequiredRuleForInvalidNotEmptyStringOrdinal()
        {
            RequiredStringRuleTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "encyclopaedia" },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.Ordinal),
               asserts: result =>
               {
                   Assert.IsTrue(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestStringRequiredRuleForInvalidNotEmptyStringInvariantCultureIgnoreCase()
        {
            RequiredStringRuleTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "Archæology" },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.InvariantCultureIgnoreCase),
               asserts: result =>
               {
                   Assert.IsFalse(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestStringRequiredRuleForInvalidNotEmptyStringOrdinalIgnoreCase()
        {
            RequiredStringRuleTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "Archæology" },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.OrdinalIgnoreCase),
               asserts: result =>
               {
                   Assert.IsFalse(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestStringRequiredRuleForInvalidNotEmptyString()
        {
            RequiredStringRuleTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "Test" },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.InvariantCulture),
               asserts: result =>
               {
                   Assert.IsTrue(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        [TestMethod]
        public void TestRequiredRuleForSpecifiedInvalidValuesForNotEmptyString()
        {
            RequiredStringRuleTestTemplate(
               validationTargetFactory: () => new FakeValidationTargetModel { StringValidationTargetProperty = "Invalid" },
               propertyToValidate: x => x.StringValidationTargetProperty,
                optionsFactory: () => new StringRequiredValidatorOptions(true, StringComparison.InvariantCulture),
               asserts: result =>
               {
                   Assert.IsTrue(result.IsValid());
                   Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                   Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                       nameof(FakeValidationTargetModel.StringValidationTargetProperty));
               });
        }

        private ValidatorDescriptor CreateDefaultValidationDescriptor()
        {
            return new ValidatorDescriptor(Guid.NewGuid(), "Default", "Deafult Message", "Descriptor");
        }

        private void RequiredStringRuleTestTemplate<TModel>(
            Func<TModel> validationTargetFactory,
            Expression<Func<TModel, string>> propertyToValidate,
            Func<StringRequiredValidatorOptions> optionsFactory,
            Action<ValidationResult> asserts) 
            where TModel: FakeValidationTargetModel
        {
            var validationTarget = validationTargetFactory();
            var ruleDescriptor = CreateDefaultValidationDescriptor();
            var targetRule = new StringRequiredValidator<TModel>(ruleDescriptor, 0,
                propertyToValidate, optionsFactory());

            ValidationResult result = targetRule.Validate(validationTarget);

            asserts(result);
        }
    }
}
