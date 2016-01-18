using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.UnitTests.Validators;
using FluentValidation.Validation.Validators;
using FluentValidation.Validation.Models.Options;
using System.Linq.Expressions;
using FluentValidation.Validation.Models.Results;
using FluentValidation.UnitTests;
using System.Collections.Generic;
using FluentValidation.Validation.Models;

namespace FluentValidation.Tests.Validators
{
    [TestClass]
    public class DeniedValuesValidatorTests
    {
        [TestMethod]
        public void TestDeniedValuesValidatorForNullValueShouldBeSucceed()
        {
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel(),
                propertyToValidate: x => x.ReferenceTypeValidationTargetProperty,
                optionsFactory: () => new ValueValidatorOptions<object>(),
                asserts: result =>
                {
                    Assert.IsTrue(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.ReferenceTypeValidationTargetProperty));
                });
        }

        [TestMethod]
        public void TestDeniedValuesValidatorForInvalidValueShouldBeFailed()
        {
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { NumericValidationTargetProperty = 1 },
                propertyToValidate: x => x.NumericValidationTargetProperty,
                optionsFactory: () => new ValueValidatorOptions<int>(new HashSet<int>(new int[] { 1 })),
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.NumericValidationTargetProperty));
                });
        }

        [TestMethod]
        public void TestDeniedValuesValidatorForValidValueShouldBeSucceed()
        {
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { NumericValidationTargetProperty = 1 },
                propertyToValidate: x => x.NumericValidationTargetProperty,
                optionsFactory: () => new ValueValidatorOptions<int>(new HashSet<int>(new int[] { 2, 3 })),
                asserts: result =>
                {
                    Assert.IsTrue(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.NumericValidationTargetProperty));
                });
        }

        [TestMethod]
        public void TestDeniedValuesValidatorForInvalidValueWithComparerShouldBeFailed()
        {
            const string invalidValues = "Invalid";
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeCompareTargetModel { ComparationProperty = new CompationModel { PropToCompare = invalidValues } },
                propertyToValidate: x => x.ComparationProperty,
                optionsFactory: () => new ValueValidatorOptions<CompationModel>(new HashSet<CompationModel>(new CompationModel[] { new CompationModel { PropToCompare = invalidValues } }), new TestComparer()),
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeCompareTargetModel.ComparationProperty));
                });
        }

        [TestMethod]
        public void TestDeniedValuesValidatorForValidValueWithComparerShouldBeSucceed()
        {
            DeniedValuesValidatorTestTemplate(
                validationTargetFactory: () => new FakeCompareTargetModel { ComparationProperty = new CompationModel { PropToCompare = Guid.NewGuid().ToString() } },
                propertyToValidate: x => x.ComparationProperty,
                optionsFactory: () => new ValueValidatorOptions<CompationModel>(new HashSet<CompationModel>(new CompationModel[] { new CompationModel { PropToCompare = Guid.NewGuid().ToString() } }), new TestComparer()),
                asserts: result =>
                {
                    Assert.IsTrue(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeCompareTargetModel.ComparationProperty));
                });
        }

        private void DeniedValuesValidatorTestTemplate<TModel, TProperty>(
                                        Func<TModel> validationTargetFactory,
                                        Expression<Func<TModel, TProperty>> propertyToValidate,
                                        Func<ValueValidatorOptions<TProperty>> optionsFactory,
                                        Action<ValidationResult> asserts)
        {
            var validationTarget = validationTargetFactory();
            var validatorDescriptor = ValidatorTestHelper.CreateDefaultLazyValueValidatorDescriptor();
            var targetValidator = new DeniedValuesValidator<TModel, TProperty>(validatorDescriptor, propertyToValidate, optionsFactory());

            ValidationResult result = targetValidator.Validate(validationTarget);

            asserts(result);
        }

        private class FakeCompareTargetModel
        {
            public CompationModel ComparationProperty { get; set; }
        }

        private class CompationModel
        {
            public string PropToCompare { get; set; }
        }

        private class TestComparer : IEqualityComparer<CompationModel>
        {
            public bool Equals(CompationModel x, CompationModel y)
            {
                return string.Equals(x.PropToCompare, y.PropToCompare);
            }

            public int GetHashCode(CompationModel obj)
            {
                if (obj == null)
                {
                    return 0;
                }

                return obj.GetHashCode();
            }
        }
    }
}
