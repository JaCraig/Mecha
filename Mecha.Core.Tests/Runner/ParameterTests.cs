using Mecha.Core.Mutator.Defaults;
using Mecha.Core.Runner;
using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Xunit;

namespace Mecha.Core.Tests.Runner
{
    /// <summary>
    /// Parameter tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Runner.Parameter}"/>
    public class ParameterTests : TestBaseClass<Parameter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterTests"/> class.
        /// </summary>
        public ParameterTests()
        {
            TestObject = new Parameter(TestMethodInfo.GetParameters()[0], 0);
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void Copy()
        {
            Parameter Result = TestObject.Copy();
            Assert.NotNull(Result);
            Assert.Equal(TestObject.ParameterInfo, Result.ParameterInfo);
            Assert.Equal(0, Result.ShrinkCount);
            Assert.Equal(Result.Value, 0);
        }

        [Fact]
        public void Mutate() => Assert.False(TestObject.Mutate(new Core.Mutator.MutatorManager(new[] { new StringMutator(Random!) }), new System.Collections.Generic.List<RunResult>()));

        [Fact]
        public void Same()
        {
            Parameter Result = TestObject.Copy();
            Assert.True(TestObject.Same(Result));
        }

        [Fact]
        public void Shrink() => Assert.False(TestObject.Shrink(new Core.Shrinker.ShrinkerManager(new[] { new NumberShrinker() }), new System.Collections.Generic.List<RunResult>()));
    }
}