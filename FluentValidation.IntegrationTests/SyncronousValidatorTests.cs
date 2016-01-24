using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Validation;
using FluentValidation.Validation.Fluent.Builders;
using System.Linq;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Validation.Models;
using Moq;
using System.Threading.Tasks;

namespace FluentValidation.IntegrationTests
{
    [TestClass]
    public class SyncronousValidatorTests
    {
        private const string RequiredKey = "Key:FakeRequired";
        private const string CollectionRequiredKey = "Key:CollectionRequired";
        private const string DeniedValuesKey = "Key:DeniedValues";
        private const int CustomInvalidValue = 100;

        [TestMethod]
        public void TestRequiredValidatorShouldBeFailed()
        {
            var modelToValidate = new FakeModel();
            var validator = CreateRequiredFactory().ResolveModel<FakeModel>();
            var result = validator.Validate(modelToValidate);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault(x => x.Key.Equals(RequiredKey));
            Assert.IsNotNull(failedValidator);
        }

        [TestMethod]
        public void TestCollectionRequiredValidatorShouldBeFailedForEmptyCollection()
        {
            var modelToValidate = new FakeModel();
            modelToValidate.RequiredCollection = new List<string>();
            var validator = CreateRequiredCollectionFactory().ResolveModel<FakeModel>();
            var result = validator.Validate(modelToValidate);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault(x => x.Key.Equals(CollectionRequiredKey));
            Assert.IsNotNull(failedValidator);
        }

        [TestMethod]
        public void TestCollectionRequiredValidatorShouldBeFailedForNullReference()
        {
            var modelToValidate = new FakeModel();
            modelToValidate.RequiredCollection = null;
            var validator = CreateRequiredCollectionFactory().ResolveModel<FakeModel>();
            var result = validator.Validate(modelToValidate);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault(x => x.Key.Equals(CollectionRequiredKey));
            Assert.IsNotNull(failedValidator);
        }

        [TestMethod]
        public void TestDeniedValueValidatorShouldBeFailedForDeniedValue()
        {
            var modelToValidate = new FakeModel();
            const int DeniedValue = 1;
            modelToValidate.DeniedValueProperty = DeniedValue;
            var validator = CreateDeniedValuesFactory(DeniedValue).ResolveModel<FakeModel>();
            var result = validator.Validate(modelToValidate);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault(x => x.Key.Equals(DeniedValuesKey));
            Assert.IsNotNull(failedValidator);
        }

        [TestMethod]
        public void TestCustomValidatorShouldBeFailed()
        {
            var validator = CreateCustomValidatorFactory(false).ResolveModel<FakeModel>();
            var result = validator.Validate(new FakeModel());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault();
            Assert.IsNotNull(failedValidator);

        }

        [TestMethod]
        public void TestAsyncCustomValidatorShouldBeFailed()
        {
            var validator = CreateCustomValidatorFactory(false).ResolveModel<FakeModel>();
            var result = validator.ValidateAsync(new FakeModel()).Result;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault();
            Assert.IsNotNull(failedValidator);

        }

        [TestMethod]
        public void TestAsyncCustomValidatorShouldBeSucceded()
        {
            var validator = CreateCustomValidatorFactory(true).ResolveModel<FakeModel>();
            var result = validator.Validate(new FakeModel());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault();
            Assert.IsNull(failedValidator);

        }

        [TestMethod]
        public void TestSyncValidatorWithRulesetOneShouldBeFailed()
        {
            string rulesetName = "customRuleset";
            var validator = CreateRulesetValidatorFactory(rulesetName).ResolveModel<FakeModel>(rulesetName);
            var result = validator.Validate(new FakeModel());

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault();
            Assert.IsNotNull(failedValidator);
        }

        private IValidationModelFactory CreateRequiredFactory()
        {
            return ValidationFactoryResolver
                .ResolveInstance()
                .RegisterConfig<FakeModel>(builder => builder
                                                 .WithValidationModel(modelBuilder =>
                                                              modelBuilder
                                                              .Required(x => x.RequiredProperty, desc => desc
                                                                        .WithKey(RequiredKey)
                                                                        .Build())
                                                              .Build())
                                                          .Build());
        }

        private IValidationModelFactory CreateRequiredCollectionFactory()
        {
            return ValidationFactoryResolver
                .ResolveInstance()
                .RegisterConfig<FakeModel>(builder => builder
                                                 .WithValidationModel(modelBuilder =>
                                                              modelBuilder
                                                              .CollectionRequired(x => x.RequiredCollection, desc => desc
                                                                        .WithKey(CollectionRequiredKey)
                                                                        .Build())
                                                              .Build())
                                                          .Build());
        }

        private IValidationModelFactory CreateDeniedValuesFactory(int deniedValue)
        {
            return ValidationFactoryResolver
                .ResolveInstance()
                .RegisterConfig<FakeModel>(builder => builder
                                                 .WithValidationModel(modelBuilder =>
                                                              modelBuilder
                                                              .DeniedValue(x => x.DeniedValueProperty,
                                                                         opt => opt
                                                                         .WithValues(new[] { deniedValue })
                                                                         .Build(),
                                                                         desc => desc
                                                                        .WithKey(DeniedValuesKey)
                                                                        .Build())
                                                              .Build())
                                                          .Build());
        }

        private IValidationModelFactory CreateCustomValidatorFactory(bool isValid)
        {
            return ValidationFactoryResolver
                .ResolveInstance()
                .RegisterConfig<FakeModel>(builder => builder
                                                 .WithValidationModel(modelBuilder =>
                                                              modelBuilder
                                                                .Custom(CreateCustomValidator(isValid))
                                                                .Build())
                                                          .Build());
        }

        private IValidationModelFactory CreateRulesetValidatorFactory(string rulesetName)
        {
            return ValidationFactoryResolver
                .ResolveInstance()
                .RegisterConfig<FakeModel>(builder => builder
                                                 .WithValidationModel(modelBuilder =>
                                                              modelBuilder
                                                                .Required(x => x.RequiredProperty)
                                                                .Build())
                                                          .Build(rulesetName))
                .RegisterConfig<FakeModel>(builder => builder
                                                 .WithValidationModel(modelBuilder =>
                                                              modelBuilder
                                                                .CollectionRequired(x => x.RequiredCollection)
                                                                .Build())
                                                          .Build());
        }

        private IValidator<FakeModel> CreateCustomValidator(bool isValid)
        {
            var mockValidator = new Mock<IValidator<FakeModel>>();
            mockValidator
                .Setup(x => x.Validate(It.IsAny<FakeModel>()))
                .Returns(new ValidationResult(isValid, It.IsAny<ValidatorDescriptor>()));
            mockValidator
                .Setup(x => x.ValidateAsync(It.IsAny<FakeModel>()))
                .Returns(Task.FromResult(new ValidationResult(isValid, It.IsAny<ValidatorDescriptor>())));
            return mockValidator.Object;
        }
    }
}
