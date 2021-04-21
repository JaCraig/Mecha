using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Negation generator tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.NegationGenerator}"/>
    public class NegationGeneratorTests : TestBaseClass<NegationGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegationGeneratorTests"/> class.
        /// </summary>
        public NegationGeneratorTests()
        {
            TestObject = new NegationGenerator(Canister.Builder.Bootstrapper.Resolve<Mirage.Random>());
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
            Assert.Equal(-min, TestObject.Next(Parameters[0], min, min));
            Assert.Equal(-max, TestObject.Next(Parameters[0], max, max));
        }
    }
}