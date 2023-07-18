using Mecha.Core.Datasources;
using Mecha.Core.Datasources.Interfaces;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System.Linq;
using Xunit;

namespace Mecha.Core.Tests.Datasources.Defaults
{
    /// <summary>
    /// Default data source tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Datasources.DefaultDatasource}"/>
    public class DefaultDatasourceTests : TestBaseClass<DefaultDatasource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDatasourceTests"/> class.
        /// </summary>
        public DefaultDatasourceTests()
        {
            TestObject = new DefaultDatasource();
            TestSerializer = new DefaultSerializer();
        }

        /// <summary>
        /// Gets the test serializer.
        /// </summary>
        /// <value>The test serializer.</value>
        private ISerializer TestSerializer { get; }

        /// <summary>
        /// Saves the and read.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        [Property]
        public void SaveAndRead(int value1, int value2)
        {
            TestObject.Save(TestMethodInfo, new object[] { value1, value2 }, TestSerializer);
            System.Collections.Generic.List<object?[]> Results = TestObject.Read(TestMethodInfo, TestSerializer);
            if (!Results.Any())
                return;
            Assert.Single(Results);
            Assert.Equal(2, Results[0].Length);
            Assert.Equal(value1, Results[0][0]);
            Assert.Equal(value2, Results[0][1]);
            TestObject.Clear(TestMethodInfo);
        }
    }
}