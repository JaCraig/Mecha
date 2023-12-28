using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System.IO;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Stream generator tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.StreamGenerator}"/>
    public class StreamGeneratorTests : TestBaseClass<StreamGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StreamGeneratorTests"/> class.
        /// </summary>
        public StreamGeneratorTests()
        {
            TestObject = new StreamGenerator(Random!);
        }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        [Property]
        public void RangeTest() => Assert.IsAssignableFrom<Stream>(TestObject.Next(null, null, null).Value);
    }
}