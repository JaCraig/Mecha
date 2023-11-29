using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Default generator tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.DefaultGenerator}"/>
    public class DefaultGeneratorTests : TestBaseClass<DefaultGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultGeneratorTests"/> class.
        /// </summary>
        public DefaultGeneratorTests()
        {
            TestObject = new DefaultGenerator(Random!);
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
            Assert.InRange((int)TestObject.Next(Parameters[0], min, max), min, max);
        }
    }
}