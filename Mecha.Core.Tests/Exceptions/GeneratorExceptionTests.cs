using Mecha.Core.Exceptions;
using Mecha.Core.Tests.BaseClasses;
using System;
using Xunit;

namespace Mecha.Core.Tests.Exceptions
{
    public class GeneratorExceptionTests : TestBaseClass<GeneratorException>
    {
        public GeneratorExceptionTests()
        {
            _Message = "TestValue270541375";
            _InnerException = new Exception();
            TestObject = new GeneratorException(_Message, _InnerException);
            Options.DiscoverInheritedMethods = false;
        }

        private readonly Exception _InnerException;
        private readonly string _Message;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new GeneratorException();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new GeneratorException(_Message);

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new GeneratorException(_Message, _InnerException);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidMessage(string? value)
        {
            _ = new GeneratorException(value);
            _ = new GeneratorException(value, _InnerException);
        }

        [Fact]
        public void CanConstructWithNullInnerException() => _ = new GeneratorException(_Message, default);
    }
}