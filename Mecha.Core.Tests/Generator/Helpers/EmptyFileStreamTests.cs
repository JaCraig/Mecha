using Mecha.Core.Generator.Helpers;
using Mecha.Core.Tests.BaseClasses;

namespace Mecha.Core.Tests.Generator.Helpers
{
    /// <summary>
    /// Empty file stream tests
    /// </summary>
    /// <seealso cref="TestBaseClass{EmptyFileStream}"/>
    public class EmptyFileStreamTests : TestBaseClass<EmptyFileStream>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyFileStreamTests"/> class.
        /// </summary>
        public EmptyFileStreamTests()
        {
            TestObject = new EmptyFileStream();
        }
    }
}