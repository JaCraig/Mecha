using Mecha.Core.Mutator.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Xunit;

namespace Mecha.Core.Tests.Mutator.Defaults
{
    /// <summary>
    /// String mutator tests
    /// </summary>
    /// <seealso cref="TestBaseClass{StringMutator}"/>
    public class StringMutatorTests : TestBaseClass<StringMutator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringMutatorTests"/> class.
        /// </summary>
        public StringMutatorTests()
        {
            TestObject = new StringMutator(Canister.Builder.Bootstrapper.Resolve<Mirage.Random>());
        }

        /// <summary>
        /// RunAsync test
        /// </summary>
        [Fact]
        public void CanMutate()
        {
            Assert.True(TestObject.CanMutate("AAAA"));
        }

        /// <summary>
        /// Mutates this instance.
        /// </summary>
        [Fact]
        public void Mutate()
        {
            var Result = (string)TestObject.Mutate("AAAA");
            Assert.Contains("\0", Result);
        }
    }
}