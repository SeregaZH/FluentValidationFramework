using FluentValidation.UnitTests;
using FluentValidation.Validation.Executors;
using FluentValidation.Validation.Models;
using FluentValidation.Validation.Models.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidation.Tests.Executors
{
    [TestClass]
    public class HierarchicalValidatorExecutorTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteSyncWithNullModelExceptionShouldBeRaised()
        {
            // arrange
            var target = new HierarchicalValidatorExecutor<FakeValidationTargetModel>();

            // act
            target.Execute(null, Enumerable.Empty<ValidatorContainer<FakeValidationTargetModel>>());

            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteSyncWithNullValidatorsCollectionExceptionShouldBeRaised()
        {
            // arrange
            var target = new HierarchicalValidatorExecutor<FakeValidationTargetModel>();

            // act
            target.Execute(new FakeValidationTargetModel(), null);

            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestExecuteAsyncWithNullModelExceptionShouldBeRaised()
        {
            // arrange
            var target = new HierarchicalValidatorExecutor<FakeValidationTargetModel>();

            // act
            var result = target.ExecuteAsync(null, Enumerable.Empty<ValidatorContainer<FakeValidationTargetModel>>()).Result;

            // expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestExecuteAsyncWithNullValidatorsCollectionExceptionShouldBeRaised()
        {
            // arrange
            var target = new HierarchicalValidatorExecutor<FakeValidationTargetModel>();

            // act
            var result = target.ExecuteAsync(new FakeValidationTargetModel(), null).Result;

            // expected exception
        }

        [TestMethod]
        public void TestExecuteSuncWithTwoInvalidValidatorWithDifferentPriorityOneValidatorShouldBeFailed()
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

            var target = new HierarchicalValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.FirstOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
            Assert.AreEqual(failedValidatorId, failedValidator.Id);
            secondMockValidator.Verify();
            firstMockValidator.Verify(x => x.Validate(It.IsAny<FakeValidationTargetModel>()), Times.Never);
        }

        [TestMethod]
        public void TestExecuteSuncWithValidValidatorShouldBeSucceed()
        {
            var fakeModel = new FakeValidationTargetModel();
            var mockValidator = ValidatorExecutorTestHelper.CreateMockValidator(true, Guid.NewGuid());
            
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                ValidatorExecutorTestHelper.CreateValidatorContainer(mockValidator.Object, 0)
            };

            var target = new HierarchicalValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidatorTask = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidatorTask);
            var failedValidator = failedValidatorTask;
            Assert.IsNotNull(failedValidator);
            Assert.IsTrue(failedValidator.IsValid());
            mockValidator.Verify();
        }

        [TestMethod]
        public void TestExecuteAsyncWithTwoInvalidValidatorWithDifferentPriorityOneValidatorShouldBeFailed()
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

            var target = new HierarchicalValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.FirstOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
            Assert.AreEqual(failedValidatorId, failedValidator.Id);
            secondMockValidator.Verify();
            firstMockValidator.Verify(x => x.Validate(It.IsAny<FakeValidationTargetModel>()), Times.Never);
        }

        [TestMethod]
        public void TestExecuteAsyncWithValidValidatorShouldBeSucceed()
        {
            var fakeModel = new FakeValidationTargetModel();
            var mockValidator = ValidatorExecutorTestHelper.CreateMockValidator(true, Guid.NewGuid());

            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                ValidatorExecutorTestHelper.CreateValidatorContainer(mockValidator.Object, 0)
            };

            var target = new HierarchicalValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            var failedValidatorTask = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidatorTask);
            var failedValidator = failedValidatorTask;
            Assert.IsNotNull(failedValidator);
            Assert.IsTrue(failedValidator.IsValid());
            mockValidator.Verify();
        }
    }
}
