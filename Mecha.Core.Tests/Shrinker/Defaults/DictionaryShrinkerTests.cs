using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using System.Collections.Generic;
using Xunit;

namespace Mecha.Core.Tests.Runner
{
    /// <summary>
    /// DictionaryShrinker tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Runner.DictionaryShrinker}"/>
    public class DictionaryShrinkerTests : TestBaseClass<DictionaryShrinker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryShrinkerTests"/> class.
        /// </summary>
        public DictionaryShrinkerTests()
        {
            TestObject = new DictionaryShrinker();
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void CanShrink()
        {
            Assert.True(TestObject.CanShrink(new Dictionary<string, string>()));
        }

        [Fact]
        public void Shrink()
        {
            var Result = (Dictionary<string, string>)TestObject.Shrink(new Dictionary<string, string> { ["A"] = "B", ["B"] = "A" });
            Assert.Single(Result);
            Assert.True(Result.TryGetValue("B", out var Value));
            Assert.Equal("A", Value);
        }
    }
}