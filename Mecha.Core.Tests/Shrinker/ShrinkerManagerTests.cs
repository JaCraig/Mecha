using Mecha.Core.Shrinker;
using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Shrinker
{
    /// <summary>
    /// ShrinkerManager tests
    /// </summary>
    /// <seealso cref="TestBaseClass{ShrinkerManager}"/>
    public class ShrinkerManagerTests : TestBaseClass<ShrinkerManager>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShrinkerManagerTests"/> class.
        /// </summary>
        public ShrinkerManagerTests()
        {
            TestObject = new ShrinkerManager(new[] { new NumberShrinker() });
        }

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        [Property]
        public void Shrink(int value)
        {
            var Result = (int)TestObject.Shrink(value);
            Assert.Equal(value / 2, Result);
        }
    }
}