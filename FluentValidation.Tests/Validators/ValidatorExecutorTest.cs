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

namespace FluentValidation.Tests.Validators
{
    [TestClass]
    public class ValidatorExecutorTest
    {
        [TestMethod]
        public void TestExecuteTroughtEmptyValidatorsCollection()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<IValidator<FakeValidationTargetModel>>();
            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            Assert.IsFalse(validationResults.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteWithNullModel()
        {
            var validators = new List<IValidator<FakeValidationTargetModel>>();
            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(null, validators);

            // Expected excuption
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteWithNullValidatorsCollection()
        {
            var fakeModel = new FakeValidationTargetModel();
            var target = new ValidatorExecutor<FakeValidationTargetModel>();

            IEnumerable<ValidationResult> validationResults = target.Execute(fakeModel, null);

            // Expected excuption
        }

        [TestMethod]
        public void TestExecuteWithInvalidValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<IValidator<FakeValidationTargetModel>> { CreateValidator(false) };
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
            var validators = new List<IValidator<FakeValidationTargetModel>> { CreateValidator(true) };
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
            var validators = new List<IValidatorAsync<FakeValidationTargetModel>> { CreateAsyncValidator(true) };
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            IEnumerable<Task<ValidationResult>> validationResults = target.ExecuteAsync(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidatorTask = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidatorTask);
            var failedValidator = failedValidatorTask.Result;
            Assert.IsNotNull(failedValidator);
            Assert.IsTrue(failedValidator.IsValid());
        }

        [TestMethod]
        public void TestExecuteAsyncWithFailedValidValidator()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<IValidatorAsync<FakeValidationTargetModel>> { CreateAsyncValidator(false) };
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            IEnumerable<Task<ValidationResult>> validationResults = target.ExecuteAsync(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            var failedValidatorTask = validationResults.SingleOrDefault();
            Assert.IsNotNull(failedValidatorTask);
            var failedValidator = failedValidatorTask.Result;
            Assert.IsNotNull(failedValidator);
            Assert.IsFalse(failedValidator.IsValid());
        }

        [TestMethod]
        public void TestExecuteAsyncWithEmptyValidatorsCollection()
        {
            var fakeModel = new FakeValidationTargetModel();
            var validators = new List<IValidatorAsync<FakeValidationTargetModel>>();
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            IEnumerable<Task<ValidationResult>> validationResults = target.ExecuteAsync(fakeModel, validators);

            Assert.IsNotNull(validationResults);
            Assert.IsFalse(validationResults.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteAsyncWithNullModel()
        {
            var validators = new List<IValidatorAsync<FakeValidationTargetModel>>();
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            IEnumerable<Task<ValidationResult>> validationResults = target.ExecuteAsync(null, validators);

            // expected exception.
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestExecuteAsyncWithNullCollection()
        {
            var target = new ValidatorExecutorAsync<FakeValidationTargetModel>();

            IEnumerable<Task<ValidationResult>> validationResults = target.ExecuteAsync(new FakeValidationTargetModel(), null);

            // expected exception.
        }

        private IValidator<FakeValidationTargetModel> CreateValidator(bool isValid)
        {
            var mockValidator = new Mock<IValidator<FakeValidationTargetModel>>();
            mockValidator.Setup(x => x.Validate(It.IsAny<FakeValidationTargetModel>()))
                .Returns(new ValidationResult(isValid, CreateDefaultValidationDescriptor()));

            return mockValidator.Object;
        }

        private IValidatorAsync<FakeValidationTargetModel> CreateAsyncValidator(bool isValid)
        {
            var mockValidator = new Mock<IValidatorAsync<FakeValidationTargetModel>>();
            mockValidator.Setup(x => x.ValidateAsync(It.IsAny<FakeValidationTargetModel>()))
                .Returns(Task.FromResult(new ValidationResult(isValid, CreateDefaultValidationDescriptor())));

            return mockValidator.Object;
        }

        private ValidatorDescriptor CreateDefaultValidationDescriptor()
        {
            return new ValidatorDescriptor(Guid.NewGuid(), "Default", "Deafult Message", "Descriptor");
        }
    }
}
