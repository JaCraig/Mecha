using Mecha.Core.Tests.BaseClasses;
using System;
using Xunit;

namespace Mecha.Core.Tests
{
    public class ArgumentThrowingClass
    {
        public void ThrowIfNull(int? A) => ArgumentNullException.ThrowIfNull(A, nameof(A));
    }

    public class ExceptionHandlerTests : TestBaseClass<ExceptionHandler>
    {
        public ExceptionHandlerTests()
        {
            TestObject = new ExceptionHandler();
        }

        [Fact]
        public void IgnoreException()
        {
            // Arrange
            var _TestObject = new ExceptionHandler();
            Exception? TestException = null;

            // Act
            _ = _TestObject.IgnoreException<Exception>();

            try
            {
                throw new Exception();
            }
            catch (Exception ex)
            {
                TestException = ex;
            }

            // Assert
            Assert.True(_TestObject.CanIgnore(TestException, GetType().GetMethod("IgnoreException")));
        }

        [Fact]
        public void IgnoreExceptionCalledFromDotThrowsX()
        {
            // Arrange
            var _TestObject = new ExceptionHandler();
            _ = _TestObject.IgnoreException<NotImplementedException>().IgnoreException<ArgumentException>();
            var Ar_TestObject = new ArgumentThrowingClass();

            // Act
            try
            {
                Ar_TestObject.ThrowIfNull(null);
            }
            catch (ArgumentNullException ex)
            {
                Assert.True(_TestObject.CanIgnore(ex, typeof(ArgumentThrowingClass).GetMethod("ThrowIfNull")));
            }
            // Assert
            Assert.False(_TestObject.CanIgnore(new ArgumentNullException(), GetType().GetMethod("IgnoreExceptionCalledFromDotThrowsX")));
        }
    }
}