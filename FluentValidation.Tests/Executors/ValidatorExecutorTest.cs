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
            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            Assert.IsFalse(validationResults.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteWithNullModel()
        {
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>();
            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(null, validators);

            // Expected exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteWithNullValidatorsCollection()
        {
            var fakeModel = new FakeValidationTargetModel();
            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, null);

            // Expected exception
        }

        [TestMethod]
        public void TestExecuteWithInvalidValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>> { CreateValidatorContainer(false, 0, Guid.NewGuid()) };
            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
        }

        [TestMethod]
        public void TestExecuteWithValidValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>> { CreateValidatorContainer(true, 0, Guid.NewGuid()) };
            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsTrue(failedValidator.IsValid());
        }

        [TestMethod]
        public void TestExecuteAsyncWithValidValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<ValidatorContainerAsync<FakeValidationTargetModel>> { CreateAsyncValidatorContainer(true, 0, Guid.NewGuid()) };
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            var failedValidatorTask = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidatorTask);
            var failedValidator = failedValidatorTask;
            Assert.IsNotNull(failedValidator);
            Assert.IsTrue(failedValidator.IsValid());
        }

        [TestMethod]
        public void TestExecuteAsyncWithFailedValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<ValidatorContainerAsync<FakeValidationTargetModel>> { CreateAsyncValidatorContainer(false, 0, Guid.NewGuid()) };
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            var failedValidatorTask = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidatorTask);
            var failedValidator = failedValidatorTask;
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
        }

        [TestMethod]
        public void TestExecuteAsyncWithEmptyValidatorsCollection()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<ValidatorContainerAsync<FakeValidationTargetModel>>();
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            Assert.IsFalse(validationResults.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestExecuteAsyncWithNullModel()
        {
            var validators = new List<ValidatorContainerAsync<FakeValidationTargetModel>>();
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(null, validators).Result;

            // expected exception.
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestExecuteAsyncWithNullCollection()
        {
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            var validationResults = target.ExecuteAsync(new FakeValidationTargetModel(), null).Result;

            // expected exception.
        }

        [TestMethod]
        public void TestExecuteWithTwoValidatorsInvalidValidatorWithHighPriority()
        {
            var fakeModel = new FakeValidationTargetModel();
            var failedValidatorId = Guid.NewGuid();
            var validators = new List<ValidatorContainer<FakeValidationTargetModel>>
            {
                CreateValidatorContainer(false, 1, Guid.NewGuid()),
                CreateValidatorContainer(false, 0, failedValidatorId)
            };

            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.FirstOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
            Assert.AreEqual(failedValidatorId, failedValidator.Id);
        }

        [TestMethod]
        public void TestExecuteWithTwoAsyncValidatorsInvalidValidatorWithHighPriority()
        {
            var fakeModel = new FakeValidationTargetModel();
            var failedValidatorId = Guid.NewGuid();
            var validators = new List<ValidatorContainerAsync<FakeValidationTargetModel>>
            {
                CreateAsyncValidatorContainer(false, 1, Guid.NewGuid()),
                CreateAsyncValidatorContainer(false, 0, failedValidatorId)
            };

            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.ExecuteAsync(fakeModel, validators).Result;

            Assert.IsNotNull(validationResults);
            var failedValidator = validationResults.FirstOrDefault();
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
            Assert.AreEqual(failedValidatorId, failedValidator.Id);
        }

        private ValidatorContainer<FakeValidationTargetModel> CreateValidatorContainer(bool isValid, long priority, Guid id)
        {
            var mockValidator = new Mock<IValidator<FakeValidationTargetModel>>();
            mockValidator.Setup(x => x.Validate(It.IsAny<FakeValidationTargetModel>()))
                .Returns(new ValidationResult(isValid, CreateDefaultValidationDescriptor(id)));

            return new ValidatorContainer<FakeValidationTargetModel>(mockValidator.Object, priority);
        }

        private ValidatorContainerAsync<FakeValidationTargetModel> CreateAsyncValidatorContainer(bool isValid, long priority, Guid id)
        {
            var mockValidator = new Mock<IValidatorAsync<FakeValidationTargetModel>>();
            mockValidator.Setup(x => x.ValidateAsync(It.IsAny<FakeValidationTargetModel>()))
                .Returns(Task.FromResult(new ValidationResult(isValid, CreateDefaultValidationDescriptor(id))));

            return new ValidatorContainerAsync<FakeValidationTargetModel>(mockValidator.Object, priority);
        }

        private ValidatorDescriptor CreateDefaultValidationDescriptor(Guid id)
        {
            return new ValidatorDescriptor(id, "Default", "Deafult Message", "Descriptor");
        }
    }
}
