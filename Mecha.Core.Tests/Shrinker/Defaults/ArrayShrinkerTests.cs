using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using Xunit;

namespace Mecha.Core.Tests.Shrinker.Defaults
{
    /// <summary>
    /// Array shrinker tests
    /// </summary>
    public class ArrayShrinkerTests : TestBaseClass<ArrayShrinker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayShrinkerTests"/> class.
        /// </summary>
        public ArrayShrinkerTests()
        {
            TestObject = new ArrayShrinker();
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void CanShrink()
        {
            Assert.False(TestObject.CanShrink(new List<string>()));
            Assert.True(TestObject.CanShrink(Array.Empty<string>()));
        }

        [Fact]
        public void Shrink()
        {
            var Result = (string[])TestObject.Shrink(new string[] { "B", "A" });
            Assert.Single(Result);
            Assert.Equal("B", Result[0]);
            Result = (string[])TestObject.Shrink(Array.Empty<string>());
            Assert.Empty(Result);
        }
    }
}