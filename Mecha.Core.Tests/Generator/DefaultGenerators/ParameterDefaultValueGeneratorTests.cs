using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System.Reflection;
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
            System.Reflection.ParameterInfo[] Parameters = TestMethodInfo.GetParameters();
            Assert.Null(TestObject.Next(Parameters[0], min, max).Value);
        }

        [Property]
        public void RangeTest2(int min, int max)
        {
            System.Reflection.MethodInfo? TestMethodInfo2 = GetType().GetMethod(nameof(TestMethod2), BindingFlags.Instance | BindingFlags.NonPublic);
            System.Reflection.ParameterInfo[] Parameters = TestMethodInfo2.GetParameters();
            Assert.Equal(100, TestObject.Next(Parameters[0], min, max).Value);
        }

        private void TestMethod2(int value = 100)
        {
        }
    }
}