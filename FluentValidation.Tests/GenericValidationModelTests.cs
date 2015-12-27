using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Validation;
using FluentValidation.Validation.Models.Results;
using Moq;
using FluentValidation.Validation.ValidationModel;
using System.Collections.Generic;
using FluentValidation.Validation.Models;
using System.Threading.Tasks;
using System.Linq;
using FluentValidation.Validation.Configuration;

namespace FluentValidation.Tests
{
    [TestClass]
    public class GenericValidationModelTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSyncValidationModelForNullModel()
        {
            ValidationModelTestTemplate(
                null,
                arrange: (model, mockExecutor, mockAsyncexecutor) => { },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncexecutor) =>
                {
                    //expected exception
                });
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void TestAsyncValidationModelForNullModel()
        {
            ValidationModelTestTemplate(
                null,
                arrange: (model, mockExecutor, mockAsyncexecutor) => { },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncexecutor) =>
                {
                    //expected exception
                });
        }

        [TestMethod]
        public void TestSyncValidationModelForInvalidSyncEmptyAsyncResults()
        {
            var targetModel = new FakeValidationTargetModel();
            var failedRuleId = Guid.NewGuid();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor, mockAsyncExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => new List<ValidationResult> { CreateValidationResult(false, failedRuleId) });

                    mockAsyncExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainerAsync<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(Enumerable.Empty<ValidationResult>()));
                },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncExecutor) => CreateGenericAssert(false, CreateSingleResultAssertion(failedRuleId))(result, mockExecutor, mockAsyncExecutor));
                
        }

        [TestMethod]
        public void TestSyncValidationModelForInvalidAsyncEmptySyncResults()
        {
            var targetModel = new FakeValidationTargetModel();
            var failedRuleId = Guid.NewGuid();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor, mockAsyncExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => Enumerable.Empty<ValidationResult>());

                    mockAsyncExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainerAsync<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(new List<ValidationResult> { CreateValidationResult(false, failedRuleId) }.AsEnumerable()));
                },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncExecutor) => CreateGenericAssert(false, CreateSingleResultAssertion(failedRuleId))(result, mockExecutor, mockAsyncExecutor));
        }

        [TestMethod]
        public void TestSyncValidationModelForValidAsyncEmptySyncResults()
        {
            var targetModel = new FakeValidationTargetModel();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor, mockAsyncExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => Enumerable.Empty<ValidationResult>());

                    mockAsyncExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainerAsync<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(new List<ValidationResult> { CreateValidationResult(true) }.AsEnumerable()));
                },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncExecutor) => CreateGenericAssert(true)(result, mockExecutor, mockAsyncExecutor));
        }

        [TestMethod]
        public void TestSyncValidationModelForValidAsyncAndSyncResults()
        {
            var targetModel = new FakeValidationTargetModel();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor, mockAsyncExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => new List<ValidationResult> { CreateValidationResult(true) });

                    mockAsyncExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainerAsync<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(new List<ValidationResult> { CreateValidationResult(true) }.AsEnumerable()));
                },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncExecutor) => CreateGenericAssert(true)(result, mockExecutor, mockAsyncExecutor));
        }

        [TestMethod]
        public void TestAsyncValidationModelForValidAsyncAndSyncResults()
        {
            var targetModel = new FakeValidationTargetModel();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor, mockAsyncExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => new List<ValidationResult> { CreateValidationResult(true) });

                    mockAsyncExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainerAsync<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(new List<ValidationResult> { CreateValidationResult(true) }.AsEnumerable()));
                },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncExecutor) => CreateGenericAssert(true)(result, mockExecutor, mockAsyncExecutor));
        }

        [TestMethod]
        public void TestAsyncValidationModelForValidAsyncEmptySyncResults()
        {
            var targetModel = new FakeValidationTargetModel();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor, mockAsyncExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => Enumerable.Empty<ValidationResult>());

                    mockAsyncExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainerAsync<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(new List<ValidationResult> { CreateValidationResult(true) }.AsEnumerable()));
                },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncExecutor) => CreateGenericAssert(true)(result, mockExecutor, mockAsyncExecutor));
        }

        [TestMethod]
        public void TestAsyncValidationModelForInvalidAsyncEmptySyncResults()
        {
            var targetModel = new FakeValidationTargetModel();
            var failedRuleId = Guid.NewGuid();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor, mockAsyncExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => Enumerable.Empty<ValidationResult>());

                    mockAsyncExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainerAsync<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(new List<ValidationResult> { CreateValidationResult(false, failedRuleId) }.AsEnumerable()));
                },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncExecutor) => CreateGenericAssert(false, CreateSingleResultAssertion(failedRuleId))(result, mockExecutor, mockAsyncExecutor));
        }

        [TestMethod]
        public void TestAsyncValidationModelForInvalidSyncEmptyAsyncResults()
        {
            var targetModel = new FakeValidationTargetModel();
            var failedRuleId = Guid.NewGuid();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor, mockAsyncExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => new List<ValidationResult> { CreateValidationResult(false, failedRuleId) });

                    mockAsyncExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainerAsync<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(Enumerable.Empty<ValidationResult>()));
                },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor, mockAsyncExecutor) => CreateGenericAssert(false, CreateSingleResultAssertion(failedRuleId))(result, mockExecutor, mockAsyncExecutor));

        }

        private void ValidationModelTestTemplate(
            FakeValidationTargetModel model,
            Action<FakeValidationTargetModel,
                Mock<IValidatorExecutor<FakeValidationTargetModel>>,
                Mock<IValidatorExecutorAsync<FakeValidationTargetModel>>> arrange,
            Func<FakeValidationTargetModel,
                IValidatorExecutor<FakeValidationTargetModel>,
                IValidatorExecutorAsync<FakeValidationTargetModel>,
                AggregateValidationResult> act,
            Action<AggregateValidationResult,
                Mock<IValidatorExecutor<FakeValidationTargetModel>>,
                Mock<IValidatorExecutorAsync<FakeValidationTargetModel>>> assert)
        {
            var mockValidatorExecutor = new Mock<IValidatorExecutor<FakeValidationTargetModel>>();
            var mockAsyncValidatorExecutor = new Mock<IValidatorExecutorAsync<FakeValidationTargetModel>>();
            arrange(model, mockValidatorExecutor, mockAsyncValidatorExecutor);
            var result = act(model, mockValidatorExecutor.Object, mockAsyncValidatorExecutor.Object);
            assert(result, mockValidatorExecutor, mockAsyncValidatorExecutor);
        }

        private ValidationResult CreateValidationResult(bool isValid, Guid? ruleId = null)
        {
            return new ValidationResult(isValid, CreateDefaultValidationDescriptor(ruleId ?? Guid.Empty));
        }

        private ValidatorDescriptor CreateDefaultValidationDescriptor(Guid ruleId)
        {
            return new ValidatorDescriptor(ruleId, "Default", "Deafult Message", "Descriptor");
        }

        private ValidationModelConfig<FakeValidationTargetModel> CreateFakeConfig()
        {
            return new ValidationModelConfig<FakeValidationTargetModel>(
                Enumerable.Empty<ValidatorContainer<FakeValidationTargetModel>>(),
                Enumerable.Empty<ValidatorContainerAsync<FakeValidationTargetModel>>());
        }

        private Action<AggregateValidationResult,
                Mock<IValidatorExecutor<FakeValidationTargetModel>>,
                Mock<IValidatorExecutorAsync<FakeValidationTargetModel>>> CreateGenericAssert(bool isValid, Action<AggregateValidationResult> resultAssertion = null)
        {
            return (result, mockExecutor, mockAsyncExecutor) =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(isValid, result.IsValid());

                if (resultAssertion != null)
                {
                    resultAssertion(result);
                }

                mockExecutor.VerifyAll();
                mockAsyncExecutor.VerifyAll();
            };
        }

        private Action<AggregateValidationResult> CreateSingleResultAssertion(Guid failedRuleId)
        {
            return result =>
            {
                var failedRule = result.FailedValidators.SingleOrDefault();
                Assert.IsNotNull(failedRule);
                Assert.AreEqual(failedRuleId, failedRule.Id);
            };
        }

        private AggregateValidationResult SyncValidationModelAct(
            FakeValidationTargetModel model, 
            IValidatorExecutor<FakeValidationTargetModel> executor, 
            IValidatorExecutorAsync<FakeValidationTargetModel> asyncExecutor)
        {
            var target = new GenericValidationModel<FakeValidationTargetModel>(executor, asyncExecutor, CreateFakeConfig());
            return target.Validate(model);
        }

        private AggregateValidationResult AsyncValidationModelAct(
           FakeValidationTargetModel model,
           IValidatorExecutor<FakeValidationTargetModel> executor,
           IValidatorExecutorAsync<FakeValidationTargetModel> asyncExecutor)
        {
            var target = new GenericValidationModel<FakeValidationTargetModel>(executor, asyncExecutor, CreateFakeConfig());
            return target.ValidateAsync(model).Result;
        }
    }
}
