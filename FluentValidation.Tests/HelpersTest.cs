using FluentValidation.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FluentValidation.Tests
{
    [TestClass]
    public class HelpersTest
    {
        [TestMethod]
        public void TestAsyncHelperTaskShouldBeExecutedSyncWithCorrectReturnValue()
        {
            int returnValue = 1, delay = 400;
            var task = CreateTask(returnValue, delay);
            int result;
            var watch = new Stopwatch();
            watch.Start();
            using (var awaiter = AsyncHelper.Wait())
            {
                result = awaiter.RunSync(() => task);
            }
            watch.Stop();

            Debug.Write(watch.ElapsedMilliseconds);
            Assert.IsTrue(watch.ElapsedMilliseconds >= delay);
            Assert.AreEqual(returnValue, result);
        }

        private async Task<int> CreateTask(int returnValue, int delay)
        {
            // just added 45 second to resolve timer resolution alignment.
            await Task.Delay(delay + 45);
            return returnValue;
        }
    }
}
