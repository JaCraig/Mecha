using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Xunit;

namespace Mecha.Core.Tests.Shrinker.Defaults
{
    /// <summary>
    /// NumberShrinker tests
    /// </summary>
    /// <seealso cref="TestBaseClass{Core.Runner.NumberShrinker}"/>
    public class NumberShrinkerTests : TestBaseClass<NumberShrinker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberShrinkerTests"/> class.
        /// </summary>
        public NumberShrinkerTests()
        {
            TestObject = new NumberShrinker();
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void CanShrink() => Assert.True(TestObject.CanShrink(1111));

        [Fact]
        public void Shrink()
        {
            var Result = (int)TestObject.Shrink(1000);
            Assert.Equal(500, Result);
        }
    }
}