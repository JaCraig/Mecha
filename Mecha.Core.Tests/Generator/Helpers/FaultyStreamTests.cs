using Mecha.Core.Generator.Helpers;
using System.IO;
using Xunit;

namespace Mecha.Core.Tests.Generator.Helpers
{
    public class FaultyStreamTests
    {
        public FaultyStreamTests()
        {
            _TestClass = new FaultyStream();
        }

        private readonly FaultyStream _TestClass;

        [Fact]
        public void AllMethodsThrowEndOfStreamException()
        {
            _ = Assert.Throws<EndOfStreamException>(_TestClass.Flush);
            _ = Assert.Throws<EndOfStreamException>(() => _TestClass.Read(null!, 0, 0));
            _ = Assert.Throws<EndOfStreamException>(() => _TestClass.Seek(0, SeekOrigin.Begin));
            _ = Assert.Throws<EndOfStreamException>(() => _TestClass.Write(null!, 0, 0));
        }

        [Fact]
        public void PropertiesSetProperly()
        {
            Assert.True(_TestClass.CanRead);
            Assert.True(_TestClass.CanSeek);
            Assert.True(_TestClass.CanWrite);
            Assert.Equal(10000, _TestClass.Length);
            Assert.Equal(0, _TestClass.Position);
            Assert.False(_TestClass.CanTimeout);
        }
    }
}