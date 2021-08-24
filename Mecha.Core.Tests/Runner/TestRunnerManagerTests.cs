using Mecha.Core.Runner;
using Mecha.Core.Tests.BaseClasses;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests.Runner
{
    /// <summary>
    /// TestRunnerManager tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Runner.TestRunnerManager}"/>
    public class TestRunnerManagerTests : TestBaseClass<TestRunnerManager>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunnerManagerTests"/> class.
        /// </summary>
        public TestRunnerManagerTests()
        {
            TestObject = new TestRunnerManager(new[] { new DefaultRunner(Random) });
        }

        /// <summary>
        /// Runs the asynchronous.
        /// </summary>
        [Fact]
        public async Task RunAsync()
        {
            var Results = await TestObject.RunAsync(TestMethodInfo, this, Options.Default).ConfigureAwait(false);
            Assert.Null(Results.Exception);
            Assert.True(Results.ExecutionTime >= 0);
            Assert.NotEmpty(Results.Output);
            Assert.True(Results.Passed);
        }
    }
}