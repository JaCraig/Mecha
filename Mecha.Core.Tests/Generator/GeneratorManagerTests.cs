using Mecha.Core.Generator;
using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Xunit;

namespace Mecha.Core.Tests.Generator
{
    /// <summary>
    /// Generator manager tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.GeneratorManager}"/>
    public class GeneratorManagerTests : TestBaseClass<GeneratorManager>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorManagerTests"/> class.
        /// </summary>
        public GeneratorManagerTests()
        {
            TestObject = new GeneratorManager(new[] { new DefaultGenerator(Canister.Builder.Bootstrapper.Resolve<Mirage.Random>()) }, Canister.Builder.Bootstrapper.Resolve<Mirage.Random>());
        }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        [Fact]
        public void RangeTest()
        {
            var Parameters = TestMethodInfo.GetParameters();
            var Values = TestObject.GenerateParameterValues(Parameters, Options.Default);
            Assert.Equal(2, Values.Length);
            Assert.Equal(10, Values[0].GeneratedValues.Count);
            Assert.Equal(10, Values[1].GeneratedValues.Count);
        }
    }
}