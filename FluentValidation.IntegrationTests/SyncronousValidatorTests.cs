﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Validation;
using FluentValidation.Validation.Fluent.Builders;
using System.Linq;

namespace FluentValidation.IntegrationTests
{
    [TestClass]
    public class SyncronousValidatorTests
    {
        private const string RequiredKey = "Key:FakeRequired";
        private const string CollectionRequiredKey = "Key:CollectionRequired";
        private const string DeniedValuesKey = "Key:DeniedValues";
        private const int DeniedValue = 1;

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
            modelToValidate.DeniedValueProperty = DeniedValue;
            var validator = CreateDeniedValuesFactory().ResolveModel<FakeModel>();
            var result = validator.Validate(modelToValidate);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidator = result.FailedValidators.SingleOrDefault(x => x.Key.Equals(DeniedValuesKey));
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

        private IValidationModelFactory CreateDeniedValuesFactory()
        {
            return ValidationFactoryResolver
                .ResolveInstance()
                .RegisterConfig<FakeModel>(builder => builder
                                                 .WithValidationModel(modelBuilder =>
                                                              modelBuilder
                                                              .DeniedValue(x => x.DeniedValueProperty,
                                                                         opt => opt
                                                                         .WithValues(new[] { DeniedValue })
                                                                         .Build(),
                                                                         desc => desc
                                                                        .WithKey(DeniedValuesKey)
                                                                        .Build())
                                                              .Build())
                                                          .Build());
        }
    }
}
