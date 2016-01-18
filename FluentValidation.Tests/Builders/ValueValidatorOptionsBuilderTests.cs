using FluentValidation.Validation.Builders.Fluent;
using FluentValidation.Validation.Models.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentValidation.Tests.Builders
{
    [TestClass]
    public class ValueValidatorOptionsBuilderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWithComparerWithNullComparerExceptionShouldBeThrew()
        {
            TestsTemplate<string>(builder => builder.WithComparer(null), result => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWithValuesWithNullCollectionExceptionShouldBeThrew()
        {
            TestsTemplate<string>(builder => builder.WithValues(null), result => { });
        }

        [TestMethod]
        public void TestWithValuesWithHashSetShouldBeSetted()
        {
            int expected = 1;
            var values = new HashSet<int>(new[] { expected });
            TestsTemplate<int>(
                builder => builder.WithValues(values), 
                result => 
                {
                    var actual = result.Values.FirstOrDefault();
                    Assert.AreEqual(expected, actual);

                });
        }

        [TestMethod]
        public void TestWithValuesWithCollectionShouldBeSetted()
        {
            int expected = 1;
            var values = new[] { expected };
            TestsTemplate<int>(
                builder => builder.WithValues(values),
                result =>
                {
                    var actual = result.Values.FirstOrDefault();
                    Assert.AreEqual(expected, actual);

                });
        }

        [TestMethod]
        public void TestWithComparerWithCollectionShouldBeSetted()
        {
            StringComparer expected = StringComparer.InvariantCulture;
            TestsTemplate<string>(
                builder => builder.WithComparer(expected),
                result =>
                {
                    var actual = result.Comparer;
                    Assert.IsInstanceOfType(actual, typeof(StringComparer));
                });
        }

        private void TestsTemplate<TType>(
            Func<IValueValidatorOptionsBuilder<TType>, IValueValidatorOptionsBuilder<TType>> act,
            Action<IValueValidatorOptions<TType>> assert)
        {
            var target = new ValueValidatorOptionsBuilder<TType>();
            var builder = act(target);
            assert(builder.Build());
        }
    }
}
