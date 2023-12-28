using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Default value generator
    /// </summary>
    /// <seealso cref="TestBaseClass{DefaultValueGenerator}"/>
    public class DefaultValueGeneratorTests : TestBaseClass<DefaultValueGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValueGeneratorTests"/> class.
        /// </summary>
        public DefaultValueGeneratorTests()
        {
            TestObject = new DefaultValueGenerator();
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
            Assert.Equal(0, (int)TestObject.Next(Parameters[0], min, max).Value);
        }
    }
}