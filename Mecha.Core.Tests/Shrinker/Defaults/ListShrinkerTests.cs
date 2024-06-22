using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using System.Collections.Generic;
using Xunit;

namespace Mecha.Core.Tests.Shrinker.Defaults
{
    /// <summary>
    /// ListShrinker tests
    /// </summary>
    /// <seealso cref="TestBaseClass{Core.Runner.ListShrinker}"/>
    public class ListShrinkerTests : TestBaseClass<ListShrinker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListShrinkerTests"/> class.
        /// </summary>
        public ListShrinkerTests()
        {
            TestObject = new ListShrinker();
            _TestClass = new ListShrinker();
        }

        private readonly ListShrinker _TestClass;

        [Fact]
        public void CanCallCanShrink()
        {
            // Arrange
            var Value = new List<string>()
            {
                "A",
                "B"
            };

            // Act
            var Result = _TestClass.CanShrink(Value);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallShrink()
        {
            // Arrange
            var Value = new List<char>();
            for (var x = char.MinValue; x < char.MaxValue; ++x)
            {
                Value.Add(x);
            }

            // Act
            var Result = (List<char>?)_TestClass.Shrink(Value);

            // Assert
            Assert.Equal(Value.Count - (Value.Count / 5), Result.Count);
            Assert.Equal('\0', Result[0]);
        }

        [Fact]
        public void CanCallShrinkWithEmptyList()
        {
            // Arrange
            var Value = new List<string>();

            // Act
            var Result = (List<string>?)_TestClass.Shrink(Value);

            // Assert
            Assert.Empty(Result!);
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void CanShrink() => Assert.True(TestObject.CanShrink(new List<string>()));

        [Fact]
        public void Shrink()
        {
            var Result = (List<string>?)TestObject.Shrink(new List<string> { "B", "A" });
            _ = Assert.Single(Result!);
            Assert.Equal("B", Result[0]);
        }
    }
}