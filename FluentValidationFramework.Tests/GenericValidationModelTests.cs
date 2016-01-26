using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidationFramework.Validation;
using FluentValidationFramework.Validation.Models.Results;
using Moq;
using FluentValidationFramework.Validation.ValidationModel;
using System.Collections.Generic;
using FluentValidationFramework.Validation.Models;
using System.Threading.Tasks;
using System.Linq;
using FluentValidationFramework.Validation.Configuration;

namespace FluentValidationFramework.UnitTests
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
                arrange: (model, mockExecutor) => { },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor) =>
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
                arrange: (model, mockExecutor) => { },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor) =>
                {
                    //expected exception
                });
        }

        [TestMethod]
        public void TestSyncValidationModelForInvalidValidatorShouldBeFailed()
        {
            var targetModel = new FakeValidationTargetModel();
            var failedRuleId = Guid.NewGuid();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => new List<ValidationResult> { CreateValidationResult(false, failedRuleId) });
                },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor) => CreateGenericAssert(false, CreateSingleResultAssertion(failedRuleId))(result, mockExecutor));
                
        }

        [TestMethod]
        public void TestSyncValidationModelForEmptyResultsShouldBeSucceed()
        {
            var targetModel = new FakeValidationTargetModel();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => Enumerable.Empty<ValidationResult>());
                },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor) => CreateGenericAssert(true)(result, mockExecutor));
        }

        [TestMethod]
        public void TestSyncValidationModelForValidResultsShouldBeSucceed()
        {
            var targetModel = new FakeValidationTargetModel();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.Execute(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => new List<ValidationResult> { CreateValidationResult(true) });
                },
                act: SyncValidationModelAct,
                assert: (result, mockExecutor) => CreateGenericAssert(true)(result, mockExecutor));
        }

        [TestMethod]
        public void TestAsyncValidationModelForValidValidatorShouldBeSucceed()
        {
            var targetModel = new FakeValidationTargetModel();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(new List<ValidationResult> { CreateValidationResult(true) } as IEnumerable<ValidationResult>));
                },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor) => CreateGenericAssert(true)(result, mockExecutor));
        }

        [TestMethod]
        public void TestAsyncValidationModelForEmptyResultsShouldBeSucceed()
        {
            var targetModel = new FakeValidationTargetModel();
            var failedRuleId = Guid.NewGuid();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() => Task.FromResult(Enumerable.Empty<ValidationResult>() as IEnumerable<ValidationResult>));
                },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor) => CreateGenericAssert(true)(result, mockExecutor));
        }

        [TestMethod]
        public void TestAsyncValidationModelForInvalidResultsShouldBeFailed()
        {
            var targetModel = new FakeValidationTargetModel();
            var failedRuleId = Guid.NewGuid();
            ValidationModelTestTemplate(
                targetModel,
                arrange: (model, mockExecutor) =>
                {
                    mockExecutor
                    .Setup(x => x.ExecuteAsync(model, It.IsAny<IEnumerable<ValidatorContainer<FakeValidationTargetModel>>>()))
                    .Returns(() =>Task.FromResult(new List<ValidationResult> { CreateValidationResult(false, failedRuleId) } as IEnumerable<ValidationResult>));
                },
                act: AsyncValidationModelAct,
                assert: (result, mockExecutor) => CreateGenericAssert(false, CreateSingleResultAssertion(failedRuleId))(result, mockExecutor));

        }

        private void ValidationModelTestTemplate(
            FakeValidationTargetModel model,
            Action<FakeValidationTargetModel,
                Mock<IValidatorExecutor<FakeValidationTargetModel>>> arrange,
            Func<FakeValidationTargetModel,
                IValidatorExecutor<FakeValidationTargetModel>,
                AggregateValidationResult> act,
            Action<AggregateValidationResult,
                Mock<IValidatorExecutor<FakeValidationTargetModel>>> assert)
        {
            var mockValidatorExecutor = new Mock<IValidatorExecutor<FakeValidationTargetModel>>();
            arrange(model, mockValidatorExecutor);
            var result = act(model, mockValidatorExecutor.Object);
            assert(result, mockValidatorExecutor);
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
            return new ValidationModelConfig<FakeValidationTargetModel>(Enumerable.Empty<ValidatorContainer<FakeValidationTargetModel>>());
        }

        private Action<AggregateValidationResult,
                Mock<IValidatorExecutor<FakeValidationTargetModel>>> CreateGenericAssert(bool isValid, Action<AggregateValidationResult> resultAssertion = null)
        {
            return (result, mockExecutor) =>
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(isValid, result.IsValid());

                if (resultAssertion != null)
                {
                    resultAssertion(result);
                }

                mockExecutor.VerifyAll();
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
            IValidatorExecutor<FakeValidationTargetModel> executor)
        {
            var target = new GenericValidationModel<FakeValidationTargetModel>(executor, CreateFakeConfig());
            return target.Validate(model);
        }

        private AggregateValidationResult AsyncValidationModelAct(
           FakeValidationTargetModel model,
           IValidatorExecutor<FakeValidationTargetModel> executor)
        {
            var target = new GenericValidationModel<FakeValidationTargetModel>(executor, CreateFakeConfig());
            return target.ValidateAsync(model).Result;
        }
    }
}
