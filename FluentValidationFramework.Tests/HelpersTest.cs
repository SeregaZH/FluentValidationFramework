using FluentValidationFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FluentValidationFramework.Tests
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void TestAsyncHelperTaskShouldBeExecutedSyncWithCorrectReturnValue()
        {
            int returnValue = 1, delay = 200;
            int result;
            var watch = new Stopwatch();
            watch.Start();
            using (var awaiter = AsyncHelper.Sync())
            {
                var task = CreateAandRunTask(returnValue, delay);
                result = awaiter.RunSync(() => task);
            }
            watch.Stop();

            Assert.IsTrue(watch.ElapsedMilliseconds >= delay);
            Assert.AreEqual(returnValue, result);
        }

        [TestMethod]
        public void TestAsyncHelperTwoTaskShouldBeExecutedSyncWithCorrectReturnValues()
        {
            int firstReturnValue = 1, secondReturnValue = 2, delay = 200;
            int firstResult, secondResult;
            var watch = new Stopwatch();

            watch.Start();
            using (var awaiter = AsyncHelper.Sync())
            {
                var firstTask = CreateAandRunTask(firstReturnValue, delay);
                firstResult = awaiter.RunSync(() => firstTask);
                var secondTask = CreateAandRunTask(secondReturnValue, delay);
                secondResult = awaiter.RunSync(() => secondTask);
            }
            watch.Stop();

            Assert.IsTrue(watch.ElapsedMilliseconds >= delay * 2);
            Assert.AreEqual(firstReturnValue, firstResult);
            Assert.AreEqual(secondReturnValue, secondResult);
        }

        private async Task<int> CreateAandRunTask(int returnValue, int delay)
        {
            // just added 45 second to resolve timer resolution alignment.
            await Task.Delay(delay + 45);
            return returnValue;
        }
    }
}
