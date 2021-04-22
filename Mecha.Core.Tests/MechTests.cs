using Mecha.Core.Tests.BaseClasses;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests.Runner
{
    /// <summary>
    /// Mech tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Shrinker.Mech}"/>
    public class MechTests : TestBaseClass<Mech>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MechTests"/> class.
        /// </summary>
        public MechTests()
        {
            TestObject = Mech.Default;
        }

        [Fact]
        public Task BreakStaticMethods()
        {
            var StaticMethods = typeof(Mech).GetMethods();
            var Tasks = new List<Task>();
            foreach (var Method in StaticMethods.Where(x => x.IsStatic && !x.IsGenericMethod))
            {
                Tasks.Add(Mech.BreakAsync(Method, null, Options.Default));
            }
            return Task.WhenAll(Tasks);
        }

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        [Fact]
        public async Task RunAsync()
        {
            var Result = await TestObject.RunAsync(TestMethodInfo, this, Options.Default).ConfigureAwait(false);
            Assert.Null(Result.Exception);
            Assert.True(Result.ExecutionTime >= 0);
            Assert.NotEmpty(Result.Output);
            Assert.True(Result.Passed);
        }
    }
}