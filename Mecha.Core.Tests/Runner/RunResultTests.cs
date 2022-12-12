using Mecha.Core.Mutator.Defaults;
using Mecha.Core.Runner;
using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Microsoft.Extensions.DependencyInjection;
using Mirage;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests.Runner
{
    /// <summary>
    /// RunResult tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Runner.RunResult}"/>
    public class RunResultTests : TestBaseClass<RunResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunResultTests"/> class.
        /// </summary>
        public RunResultTests()
        {
            TestObject = new RunResult(TestMethodInfo, this, new object[] { 0, 0 });
        }

        /// <summary>
        /// Copies this instance.
        /// </summary>
        [Fact]
        public void Copy()
        {
            var Result = TestObject.Copy();
            Assert.NotNull(Result);
            Assert.Equal(TestObject.ElapsedTime, Result.ElapsedTime);
            Assert.Equal(TestObject.Exception, Result.Exception);
            Assert.Equal(TestObject.Method, Result.Method);
            Assert.Equal(TestObject.Parameters.Length, Result.Parameters.Length);
            Assert.Equal(TestObject.ReturnedValue, Result.ReturnedValue);
            Assert.Equal(TestObject.ShrinkCount, Result.ShrinkCount);
            Assert.Equal(TestObject.Target, Result.Target);
        }

        /// <summary>
        /// Shrinks this instance.
        /// </summary>
        [Fact]
        public void Mutate()
        {
            Assert.False(TestObject.Mutate(new Core.Mutator.MutatorManager(new[] { new StringMutator(new ServiceCollection().AddCanisterModules()?.BuildServiceProvider()?.GetService<Random>()) }), new System.Collections.Generic.List<RunResult>(), Options.Default));
        }

        /// <summary>
        /// Runs the asynchronous.
        /// </summary>
        [Fact]
        public async Task RunAsync()
        {
            Assert.True(await TestObject.RunAsync(new System.Diagnostics.Stopwatch(), Options.Default).ConfigureAwait(false));
        }

        /// <summary>
        /// Runs the with ignored exception asynchronous.
        /// </summary>
        [Fact]
        public async Task RunWithIgnoredExceptionAsync()
        {
            Assert.True(await new RunResult(TestMethodWithExceptionInfo, this, new object[] { 0 }).RunAsync(new System.Diagnostics.Stopwatch(), Options.Default).ConfigureAwait(false));
        }

        /// <summary>
        /// Sames this instance.
        /// </summary>
        [Fact]
        public void Same()
        {
            var Result = TestObject.Copy();
            Assert.True(TestObject.Same(Result));
        }

        /// <summary>
        /// Shrinks this instance.
        /// </summary>
        [Fact]
        public void Shrink()
        {
            Assert.False(TestObject.Shrink(new Core.Shrinker.ShrinkerManager(new[] { new NumberShrinker() }), new System.Collections.Generic.List<RunResult>(), Options.Default));
        }
    }
}