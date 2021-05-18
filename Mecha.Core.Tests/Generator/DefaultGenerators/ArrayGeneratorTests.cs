using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System;
using System.Reflection;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Array generator tests
    /// </summary>
    /// <seealso cref="TestBaseClass{ArrayGenerator}"/>
    public class ArrayGeneratorTests : TestBaseClass<ArrayGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayGeneratorTests"/> class.
        /// </summary>
        public ArrayGeneratorTests()
        {
            TestObject = new ArrayGenerator(Canister.Builder.Bootstrapper.Resolve<Mirage.Random>());
            TestMethodInfo2 = Array.Find(GetType().GetMethods(), x => x.Name == "TestMethod2");
        }

        /// <summary>
        /// Gets the test method info2.
        /// </summary>
        /// <value>The test method info2.</value>
        private MethodInfo? TestMethodInfo2 { get; }

        /// <summary>
        /// Determines whether this instance can generate.
        /// </summary>
        [Fact]
        public void CanGenerate()
        {
            Assert.True(TestObject.CanGenerate(TestMethodInfo2.GetParameters()[0]));
        }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        [Property]
        public void RangeTest(int testNumber)
        {
            var Parameters = TestMethodInfo2.GetParameters();
            Assert.InRange(((int[])TestObject.Next(Parameters[0], 0, 50)).Length, 0, 100);
        }

        /// <summary>
        /// Tests the method2.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public void TestMethod2(params int[] args)
        {
        }
    }
}