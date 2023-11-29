using Mecha.Core.Datasources;
using Mecha.Core.Datasources.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System.Linq;
using Xunit;

namespace Mecha.Core.Tests.Datasources
{
    /// <summary>
    /// Data manager tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Datasources.DataManager}"/>
    public class DataManagerTests : TestBaseClass<DataManager>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataManagerTests"/> class.
        /// </summary>
        public DataManagerTests()
        {
            TestObject = new DataManager(new[] { new DefaultDatasource() }, new[] { new DefaultSerializer() });
        }

        /// <summary>
        /// Serializes the and deserialize.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        [Property]
        public void SerializeAndDeserialize(int value1, int value2)
        {
            TestObject.Save(TestMethodInfo!, new object?[] { value1, value2 });
            System.Collections.Generic.List<object?[]> Results = TestObject.Read(TestMethodInfo!);
            if (!Results.Any())
                return;
            _ = Assert.Single(Results);
            Assert.Equal(2, Results[0].Length);
            Assert.Equal(value1, Results[0][0]);
            Assert.Equal(value2, Results[0][1]);
            TestObject.Clear(TestMethodInfo!);
        }
    }
}