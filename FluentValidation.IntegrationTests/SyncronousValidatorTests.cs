using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Validation;
using FluentValidation.Validation.Fluent.Builders;
using System.Linq;

namespace FluentValidation.IntegrationTests
{
    [TestClass]
    public class SyncronousValidatorTests
    {
        private IValidationModelFactory _factory;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _factory = ValidationFactoryResolver.Resolve();

            _factory.RegisterConfig<FakeModel>(builder => builder
                                                          .WithValidationModel(modelBuilder =>
                                                              modelBuilder
                                                              .Required(x => x.RequiredProperty)
                                                              .Build())
                                                          .Build());
        }
        
        [TestMethod]
        public void TestRequiredValidatorShouldBeFailed()
        {
            var modelToValidate = new FakeModel();
            var validator = _factory.ResolveModel<FakeModel>();
            var result = validator.Validate(modelToValidate);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid());
            var failedValidationResult = result.FailedValidators.FirstOrDefault();
            Assert.IsNotNull(failedValidationResult);            
        }
    }
}
