using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using NSubstitute;
using System;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Mecha.Core.Tests
{
    public class PropertyDiscovererTests
    {
        public PropertyDiscovererTests()
        {
            _MessageSink = Substitute.For<IMessageSink>();
            _TestClass = new PropertyDiscoverer(_MessageSink);
        }

        private readonly IMessageSink _MessageSink;
        private readonly PropertyDiscoverer _TestClass;

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new PropertyDiscoverer();

            // Assert
            Assert.NotNull(Instance);

            // Act
            Instance = new PropertyDiscoverer(_MessageSink);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CannotCallDiscoverWithNullDiscoveryOptions()
        {
            Assert.Throws<NullReferenceException>(() => _TestClass.Discover(default, Substitute.For<ITestMethod>(), Substitute.For<IAttributeInfo>()));
        }

        [Fact]
        public void CannotCallDiscoverWithNullFactAttribute()
        {
            Assert.Throws<ArgumentException>(() => _TestClass.Discover(Substitute.For<ITestFrameworkDiscoveryOptions>(), Substitute.For<ITestMethod>(), default));
        }

        [Fact]
        public void CannotCallDiscoverWithNullTestMethod()
        {
            Assert.Throws<ArgumentException>(() => _TestClass.Discover(Substitute.For<ITestFrameworkDiscoveryOptions>(), default, Substitute.For<IAttributeInfo>()));
        }

        [Fact]
        public void MessageSinkIsInitializedCorrectly()
        {
            Assert.Same(_MessageSink, _TestClass.MessageSink);
        }
    }
}