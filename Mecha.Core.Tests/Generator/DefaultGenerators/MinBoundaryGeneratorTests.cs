using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Min boundary generator tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.MinBoundaryGenerator}"/>
    public class MinBoundaryGeneratorTests : TestBaseClass<MinBoundaryGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinBoundaryGeneratorTests"/> class.
        /// </summary>
        public MinBoundaryGeneratorTests()
        {
            TestObject = new MinBoundaryGenerator();
        }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        [Property]
        public void RangeTest(int min, int max)
        {
            System.Reflection.ParameterInfo[] Parameters = TestMethodInfo.GetParameters();
            Assert.Equal(int.MinValue, TestObject.Next(Parameters[0], min, max));
        }
    }
}