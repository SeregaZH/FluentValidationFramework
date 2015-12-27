using System;
using System.Text;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Validation.Models;

namespace FluentValidation.Tests.Validators
{
    [TestClass]
    public class CollectionRequiredValidatorTests
    {
        [TestMethod]
        public void TestCollectionRequiredValidatorWithNullCollection()
        {
            CollectionRequiredValidatorTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel(),
                propertyToValidate: x => x.CollectionValidationTargetProperty,
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.CollectionValidationTargetProperty));
                });
        }

        [TestMethod]
        public void TestCollectionRequiredValidatorWithEmptyCollection()
        {
            CollectionRequiredValidatorTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { CollectionValidationTargetProperty = new List<object>() },
                propertyToValidate: x => x.CollectionValidationTargetProperty,
                asserts: result =>
                {
                    Assert.IsFalse(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.CollectionValidationTargetProperty));
                });
        }

        [TestMethod]
        public void TestCollectionRequiredValidatorWithNotEmptyCollection()
        {
            CollectionRequiredValidatorTemplate(
                validationTargetFactory: () => new FakeValidationTargetModel { CollectionValidationTargetProperty = new List<object> { new object() } },
                propertyToValidate: x => x.CollectionValidationTargetProperty,
                asserts: result =>
                {
                    Assert.IsTrue(result.IsValid());
                    Assert.IsInstanceOfType(result, typeof(PropertyValidationResult));
                    Assert.AreEqual(((PropertyValidationResult)result).PropertyName,
                        nameof(FakeValidationTargetModel.CollectionValidationTargetProperty));
                });
        }

        private void CollectionRequiredValidatorTemplate<TModel, TProperty>(
            Func<TModel> validationTargetFactory,
            Expression<Func<TModel, IEnumerable<TProperty>>> propertyToValidate,
            Action<ValidationResult> asserts)
        {
            var validationTarget = validationTargetFactory();
            var validatorDescriptor = ValidatorTestHelper.CreateDefaultValidationDescriptor();
            var validator = new CollectionRequiredValidator<TModel, TProperty>(validatorDescriptor, propertyToValidate);
            var validationResult = validator.Validate(validationTarget);
            asserts(validationResult);
        }
    }
}
