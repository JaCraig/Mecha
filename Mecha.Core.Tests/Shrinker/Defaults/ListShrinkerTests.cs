using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using System.Collections.Generic;
using Xunit;

namespace Mecha.Core.Tests.Runner
{
    /// <summary>
    /// ListShrinker tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Runner.ListShrinker}"/>
    public class ListShrinkerTests : TestBaseClass<ListShrinker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListShrinkerTests"/> class.
        /// </summary>
        public ListShrinkerTests()
        {
            TestObject = new ListShrinker();
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void CanShrink()
        {
            Assert.True(TestObject.CanShrink(new List<string>()));
        }

        [Fact]
        public void Shrink()
        {
            var Result = (List<string>)TestObject.Shrink(new List<string> { "B", "A" });
            Assert.Single(Result);
            Assert.Equal("B", Result[0]);
        }
    }
}