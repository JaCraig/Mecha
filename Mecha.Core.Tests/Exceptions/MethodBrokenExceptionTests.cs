using Mecha.Core.Exceptions;
using Mecha.Core.Tests.BaseClasses;
using System;
using Xunit;

namespace Mecha.Core.Tests.Exceptions
{
    public class MethodBrokenExceptionTests : TestBaseClass<MethodBrokenException>
    {
        public MethodBrokenExceptionTests()
        {
            _Message = "TestValue56200931";
            _InnerException = new Exception();
            TestObject = new MethodBrokenException(_Message, _InnerException);
            Options.DiscoverInheritedMethods = false;
        }

        private readonly Exception _InnerException;
        private readonly string _Message;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new MethodBrokenException();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new MethodBrokenException(_Message);

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new MethodBrokenException(_Message, _InnerException);

            // Assert
            Assert.NotNull(Instance);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CanConstructWithInvalidMessage(string value)
        {
            _ = new MethodBrokenException(value);
            _ = new MethodBrokenException(value, _InnerException);
        }
    }
}