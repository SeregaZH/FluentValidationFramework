using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Validation.Models.Results;
using FluentValidation.Validation.Models;
using System.Linq;

namespace FluentValidation.Tests.Validators
{
    [TestClass]
    public class AggregateValidationResultTests
    {
        [TestMethod]
        public void TestAggregationShouldBeFailedIfAnyRuleFailed()
        {
            var failedRuleId = Guid.NewGuid();
            var passedRuleId = Guid.NewGuid();

            var failedRules = new List<ValidationResult>
            {
                new ValidationResult(false, new ValidatorDescriptor(failedRuleId, "Key1", "M1", "D1")),
                new ValidationResult(true, new ValidatorDescriptor(passedRuleId, "Key2", "M2", "D2"))
            };
            var target = new AggregateValidationResult(failedRules);

            var result = target.IsValid();

            Assert.IsFalse(result);
            Assert.IsTrue(target.FailedValidators.Any());
            Assert.AreEqual(target.FailedValidators.First().Id, failedRuleId);
        }

        [TestMethod]
        public void TestAggregationShouldBePassedIfAllRulesPassed()
        {
            var passedRules = new List<ValidationResult>
            {
                new ValidationResult(true, new ValidatorDescriptor(Guid.NewGuid(), "Key1", "M1", "D1")),
                new ValidationResult(true, new ValidatorDescriptor(Guid.NewGuid(), "Key2", "M2", "D2"))
            };
            var target = new AggregateValidationResult(passedRules);

            var result = target.IsValid();

            Assert.IsTrue(result);
            Assert.IsFalse(target.FailedValidators.Any());
        }
    }
}
