using Mecha.Core.Runner;
using Mecha.Core.Tests.BaseClasses;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests.Runner
{
    /// <summary>
    /// Default runner tests
    /// </summary>
    /// <seealso cref="TestBaseClass{DefaultRunner}"/>
    public class DefaultRunnerTests : TestBaseClass<DefaultRunner>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRunnerTests"/> class.
        /// </summary>
        public DefaultRunnerTests()
        {
            TestObject = new DefaultRunner(Random!);
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public async Task RunAsync()
        {
            Result Result = await TestObject.RunAsync(TestMethodInfo, this, Options.Default);
            Assert.Null(Result.Exception);
            Assert.True(Result.ExecutionTime >= 0);
            Assert.NotEmpty(Result.Output ?? "");
            Assert.True(Result.Passed);
        }
    }
}