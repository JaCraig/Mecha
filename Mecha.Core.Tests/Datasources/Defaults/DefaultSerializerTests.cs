using Mecha.Core.Datasources.Defaults;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests.Datasources.Defaults
{
    /// <summary>
    /// Default serializer tests
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{DefaultSerializer}"/>
    public class DefaultSerializerTests : TestBaseClass<DefaultSerializer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSerializerTests"/> class.
        /// </summary>
        public DefaultSerializerTests()
        {
            TestObject = new DefaultSerializer();
        }

        /// <summary>
        /// Infinites the test.
        /// </summary>
        [Fact]
        public void InfiniteTest()
        {
            Assert.Null(TestObject.Deserialize(typeof(double), TestObject.Serialize(typeof(double), double.PositiveInfinity)));
            Assert.Null(TestObject.Deserialize(typeof(float), TestObject.Serialize(typeof(float), float.PositiveInfinity)));
            Assert.Null(TestObject.Deserialize(typeof(double), TestObject.Serialize(typeof(double), double.NegativeInfinity)));
            Assert.Null(TestObject.Deserialize(typeof(float), TestObject.Serialize(typeof(float), float.NegativeInfinity)));
        }

        /// <summary>
        /// Serializes the and deserialize.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        [Property]
        public void SerializeAndDeserialize(int value1, int value2)
        {
            Assert.Equal(value1, TestObject.Deserialize(typeof(int), TestObject.Serialize(typeof(int), value1)));
            Assert.Equal(value2, TestObject.Deserialize(typeof(int), TestObject.Serialize(typeof(int), value2)));
        }
    }
}