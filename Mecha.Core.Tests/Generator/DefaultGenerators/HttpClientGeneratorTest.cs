using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Http client generator tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.HttpClientGenerator}"/>
    public class HttpClientGeneratorTest : TestBaseClass<HttpClientGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientGeneratorTest"/> class.
        /// </summary>
        public HttpClientGeneratorTest()
        {
            TestObject = new HttpClientGenerator(new ServiceCollection().AddCanisterModules()?.BuildServiceProvider()?.GetService<Mirage.Random>());
        }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        [Property]
        public void RangeTest()
        {
            Assert.IsAssignableFrom<HttpClient>(TestObject.Next(null, null, null));
        }
    }
}