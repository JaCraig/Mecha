using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// max boundary generator test
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.MaxBoundaryGenerator}"/>
    public class MaxBoundaryGeneratorTests : TestBaseClass<MaxBoundaryGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxBoundaryGeneratorTests"/> class.
        /// </summary>
        public MaxBoundaryGeneratorTests()
        {
            TestObject = new MaxBoundaryGenerator();
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
            Assert.Equal(int.MaxValue, TestObject.Next(Parameters[0], min, max).Value);
        }
    }
}