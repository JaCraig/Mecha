using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// File stream generator tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.FileStreamGenerator}"/>
    public class FileStreamGeneratorTests : TestBaseClass<FileStreamGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileStreamGeneratorTests"/> class.
        /// </summary>
        public FileStreamGeneratorTests()
        {
            TestObject = new FileStreamGenerator(new ServiceCollection().AddCanisterModules()?.BuildServiceProvider()?.GetService<Mirage.Random>());
        }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        [Property]
        public void RangeTest() => Assert.IsAssignableFrom<FileStream>(TestObject.Next(null, null, null));
    }
}