using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Xunit;

namespace Mecha.Core.Tests.Shrinker.Defaults
{
    /// <summary>
    /// StringShrinker tests
    /// </summary>
    /// <seealso cref="TestBaseClass{Core.Runner.StringShrinker}"/>
    public class StringShrinkerTests : TestBaseClass<StringShrinker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringShrinkerTests"/> class.
        /// </summary>
        public StringShrinkerTests()
        {
            TestObject = new StringShrinker();
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void CanShrink() => Assert.True(TestObject.CanShrink("AAAA"));

        [Fact]
        public void Shrink()
        {
            var Result = (string)TestObject.Shrink("AAAA");
            Assert.Equal("AAA", Result);
        }
    }
}