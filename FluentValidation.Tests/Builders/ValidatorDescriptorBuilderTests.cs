using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Validation.Fluent.Builders;
using FluentValidation.Validation.Models;

namespace FluentValidation.Tests.Builders
{
    [TestClass]
    public class ValidatorDescriptorBuilderTests
    {
        [TestMethod]
        public void TestBuildKeyWithValidKeyShouldBeSettedCorrectly()
        {
            const string validKey = "Key:Valid";
            ValidatorDescriptorBuilderTestsTemplate(
                x => x.Key(validKey),
                res => Assert.AreEqual(validKey, res.Key));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildKeyWithNullKeyArgumentExceptionShouldBeRose()
        {
            ValidatorDescriptorBuilderTestsTemplate(
                x => x.Key(null),
                res => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildKeyWithEmptyKeyArgumentExceptionShouldBeRose()
        {
            ValidatorDescriptorBuilderTestsTemplate(
                x => x.Key(string .Empty),
                res => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestBuildKeyWithWhiteSpaceKeyArgumentExceptionShouldBeRose()
        {
            ValidatorDescriptorBuilderTestsTemplate(
                x => x.Key("       "),
                res => { });
        }

        [TestMethod]
        public void TestBuildKeyWithValidDescriptionShouldBeSettedCorrectly()
        {
            const string validDsc = "description";
            ValidatorDescriptorBuilderTestsTemplate(
                x => x.Descriptor(validDsc),
                res => Assert.AreEqual(validDsc, res.Description));
        }

        [TestMethod]
        public void TestBuildKeyWithValidErrorMessageShouldBeSettedCorrectly()
        {
            const string errorMessage = "message";
            ValidatorDescriptorBuilderTestsTemplate(
                x => x.ErrorMessage(errorMessage),
                res => Assert.AreEqual(errorMessage, res.ErrorMessage));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBuildKeyWithNullErrorMessageArgumentExceptionShouldBeRose()
        {
            ValidatorDescriptorBuilderTestsTemplate(
                x => x.ErrorMessage(null),
                res => { });
        }

        private void ValidatorDescriptorBuilderTestsTemplate(
            Func<ValidatorDescriptorBuilder, ValidatorDescriptorBuilder> act, 
            Action<ValidatorDescriptor> assert)
        {
            var target = new ValidatorDescriptorBuilder();
            var builder = act(target);
            assert(builder.Build());
        }
    }
}
