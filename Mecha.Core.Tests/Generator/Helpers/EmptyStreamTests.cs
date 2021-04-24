using Mecha.Core.Generator.Helpers;
using Mecha.Core.Tests.BaseClasses;

namespace Mecha.Core.Tests.Generator.Helpers
{
    /// <summary>
    /// Empty stream tests
    /// </summary>
    /// <seealso cref="TestBaseClass{EmptyStreamTests}"/>
    public class EmptyStreamTests : TestBaseClass<EmptyStream>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyStreamTests"/> class.
        /// </summary>
        public EmptyStreamTests()
        {
            TestObject = new EmptyStream();
        }
    }
}