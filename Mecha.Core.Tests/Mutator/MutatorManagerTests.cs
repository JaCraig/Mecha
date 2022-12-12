using Mecha.Core.Mutator;
using Mecha.Core.Mutator.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Mecha.Core.Tests.Mutator
{
    /// <summary>
    /// Mutator manager tests
    /// </summary>
    /// <seealso cref="TestBaseClass{MutatorManager}"/>
    public class MutatorManagerTests : TestBaseClass<MutatorManager>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutatorManagerTests"/> class.
        /// </summary>
        public MutatorManagerTests()
        {
            TestObject = new MutatorManager(new[] { new StringMutator(new ServiceCollection().AddCanisterModules()?.BuildServiceProvider()?.GetService<Mirage.Random>()) });
        }

        /// <summary>
        /// Mutates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        [Property]
        public void Mutate([Required] string value)
        {
            var Result = (string)TestObject.Mutate(value);
            Assert.Contains("\0", Result);
        }
    }
}