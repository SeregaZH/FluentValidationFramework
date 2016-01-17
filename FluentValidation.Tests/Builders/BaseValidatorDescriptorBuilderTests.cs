using FluentValidation.Validation.Builders.Fluent;
using FluentValidation.Validation.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FluentValidation.Tests.Builders
{
    [TestClass]
    public class BaseValidatorDescriptorBuilderTests
    {
        [TestMethod]
        public void TestBuildKeyWithValidDescriptionShouldBeSettedCorrectly()
        {
            const string validDsc = "description";
            TestsTemplate(
                x => x.WithPropertyDescriptor((p) => validDsc),
                res => Assert.AreEqual(validDsc, res.DescriptionResolver(new PropertyName("Test"))));
        }

        [TestMethod]
        public void TestBuildKeyWithValidErrorMessageShouldBeSettedCorrectly()
        {
            const string errorMessage = "message";
            TestsTemplate(
                x => x.WithPropertyErrorMessage((p) => errorMessage),
                res => Assert.AreEqual(errorMessage, res.ErrorMessageResolver(new PropertyName("Test"))));
        }

        [TestMethod]
        public void TestBuildKeyWithValidKeyShouldBeSettedCorrectly()
        {
            const string validKey = "Key:Valid";
            TestsTemplate(
                x => x.WithKey(validKey),
                res => Assert.AreEqual(validKey, res.Key));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildKeyWithNullKeyArgumentExceptionShouldBeRose()
        {
            TestsTemplate(
                x => x.WithKey(null),
                res => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildKeyWithEmptyKeyArgumentExceptionShouldBeRose()
        {
            TestsTemplate(
                x => x.WithKey(string.Empty),
                res => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildKeyWithWhiteSpaceKeyArgumentExceptionShouldBeRose()
        {
            TestsTemplate(
                x => x.WithKey("       "),
                res => { });
        }

        private void TestsTemplate(
            Func<FakeValidatorDescriptorBuilder, FakeValidatorDescriptorBuilder> act,
            Action<BaseLazyValidatorDescriptor<Func<PropertyName, string>>> assert)
        {
            var target = new FakeValidatorDescriptorBuilder();
            var builder = act(target);
            assert(builder.Build());
        }

        private class FakeValidatorDescriptorBuilder : BaseValidatorDescriptorBuilder<FakeValidatorDescriptorBuilder, Func<PropertyName, string>>
        {
        }
    }
}
