using FluentValidation.Tests.Executors;
using FluentValidation.Validation;
using FluentValidation.Validation.Executors;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentValidation.UnitTests.Executors
{
    [TestClass]
    public class ValidatorExecutorTest
    {
        [TestMethod]
        public void TestExecuteTroughtEmptyValidatorsCollection()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>();
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            Assert.IsFalse(validationResults.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteWithNullModel()
        {
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>();
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(null, validators);

            // Expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteWithNullValidatorsCollection()
        {
            var fakeModel = new FakeValidationTargetModel();
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, null);

            // Expected exception
        }

        [TestMethod]
        public void TestExecuteWithInvalidValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var mockValidator = ValidatorExecutorTestHelper.CreateMockValidator(false, Guid.NewGuid());
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                ValidatorExecutorTestHelper.CreateValidatorContainer(mockValidator.Object, 0)
            };
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
            mockValidator.Verify();
        }

        [TestMethod]
        public void TestExecuteWithValidValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var mockValidator = ValidatorExecutorTestHelper.CreateMockValidator(true, Guid.NewGuid());
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                ValidatorExecutorTestHelper.CreateValidatorContainer(mockValidator.Object, 0)
            };
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsTrue(failedValidator.IsValid());
            mockValidator.Verify();
        }

        [TestMethod]
        public void TestExecuteAsyncWithValidValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var mockValidator = ValidatorExecutorTestHelper.CreateMockValidator(true, Guid.NewGuid());
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                ValidatorExecutorTestHelper.CreateValidatorContainer(mockValidator.Object, 0)
            };
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            var failedValidatorTask = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidatorTask);
            var failedValidator = failedValidatorTask;
            Assert.IsNotNull(failedValidator);
            Assert.IsTrue(failedValidator.IsValid());
            mockValidator.Verify();
        }

        [TestMethod]
        public void TestExecuteAsyncWithFailedValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var mockValidator = ValidatorExecutorTestHelper.CreateMockValidator(false, Guid.NewGuid());
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                ValidatorExecutorTestHelper.CreateValidatorContainer(mockValidator.Object, 0)
            };
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            var failedValidatorTask = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidatorTask);
            var failedValidator = failedValidatorTask;
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
            mockValidator.Verify();
        }

        [TestMethod]
        public void TestExecuteAsyncWithEmptyValidatorsCollection()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>();
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            Assert.IsFalse(validationResults.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestExecuteAsyncWithNullModel()
        {
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>();
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(null, validators).Result;

            // expected exception.
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestExecuteAsyncWithNullCollection()
        {
            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(new FakeValidationTargetModel(), null).Result;

            // expected exception.
        }

        [TestMethod]
        public void TestExecuteWithTwoValidatorsInvalidValidatorWithHighPriority()
        {
            var fakeModel = new FakeValidationTargetModel();
            var failedValidatorId = Guid.NewGuid();
            var firstMockValidator = ValidatorExecutorTestHelper.CreateMockValidator(false, Guid.NewGuid());
            var secondMockValidator = ValidatorExecutorTestHelper.CreateMockValidator(false, failedValidatorId);
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                ValidatorExecutorTestHelper.CreateValidatorContainer(firstMockValidator.Object, 1),
                ValidatorExecutorTestHelper.CreateValidatorContainer(secondMockValidator.Object, 0)
            };

            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.FirstOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
            Assert.AreEqual(failedValidatorId, failedValidator.Id);
            firstMockValidator.Verify();
            secondMockValidator.Verify();
        }

        [TestMethod]
        public void TestExecuteWithTwoAsyncValidatorsInvalidValidatorWithHighPriority()
        {
            var fakeModel = new FakeValidationTargetModel();
            var failedValidatorId = Guid.NewGuid();
            var firstMockValidator = ValidatorExecutorTestHelper.CreateMockValidator(false, Guid.NewGuid());
            var secondMockValidator = ValidatorExecutorTestHelper.CreateMockValidator(false, failedValidatorId);
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                ValidatorExecutorTestHelper.CreateValidatorContainer(firstMockValidator.Object, 1),
                ValidatorExecutorTestHelper.CreateValidatorContainer(secondMockValidator.Object, 0)
            };

            var target = new PlainValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.FirstOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
            Assert.AreEqual(failedValidatorId, failedValidator.Id);
            firstMockValidator.Verify();
            secondMockValidator.Verify();
        }
    }
}
