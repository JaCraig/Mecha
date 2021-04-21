using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Parameter default value generator tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.ParameterDefaultValueGenerator}"/>
    public class ParameterDefaultValueGeneratorTests : TestBaseClass<ParameterDefaultValueGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterDefaultValueGeneratorTests"/> class.
        /// </summary>
        public ParameterDefaultValueGeneratorTests()
        {
            TestObject = new ParameterDefaultValueGenerator();
        }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        [Property]
        public void RangeTest(int min, int max)
        {
            var Parameters = TestMethodInfo.GetParameters();
            Assert.Equal(Parameters[0].DefaultValue, TestObject.Next(Parameters[0], min, max));
        }
    }
}