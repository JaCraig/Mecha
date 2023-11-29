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
        /// Nullables the range test.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        [Property]
        public void NullableRangeTest(int? min, int? max)
        {
            System.Reflection.ParameterInfo[] Parameters = TestMethodInfo.GetParameters();
            Assert.NotNull(TestObject.Next(Parameters[0], min, max));
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
                (max, min) = (min, max);
            }
            System.Reflection.ParameterInfo[] Parameters = TestMethodInfo.GetParameters();
            Assert.Equal((min / 2) + (max / 2), TestObject.Next(Parameters[0], min, max));
        }
    }
}