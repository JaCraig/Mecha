using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Slice generator tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.SliceGenerator}"/>
    public class SliceGeneratorTests : TestBaseClass<SliceGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SliceGeneratorTests"/> class.
        /// </summary>
        public SliceGeneratorTests()
        {
            TestObject = new SliceGenerator();
        }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        [Property]
        public void RangeTest(int min, int max)
        {
            if (min > max)
            {
                var Value = min;
                min = max;
                max = Value;
            }
            var Parameters = TestMethodInfo.GetParameters();
            Assert.Equal((min / 2 + max / 2), TestObject.Next(Parameters[0], min, max));
        }
    }
}