using Mecha.Core.ExtensionMethods;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System;
using Xunit;

namespace Mecha.Core.Tests.ExtensionMethods
{
    /// <summary>
    /// Utils extensions tests
    /// </summary>
    /// <seealso cref="TestBaseClass"/>
    public class UtilsExtensionsTests : TestBaseClass
    {
        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type ObjectType => typeof(UtilsExtensions);

        /// <summary>
        /// Infinities the test.
        /// </summary>
        [Fact]
        public void InfinityTest()
        {
            Assert.True(double.NegativeInfinity.IsInfinite());
            Assert.True(double.PositiveInfinity.IsInfinite());
        }

        [Property]
        public void NotInfiniteTest(double value)
        {
            if (value is double.NegativeInfinity or double.PositiveInfinity)
                return;
            Assert.False(value.IsInfinite());
        }
    }
}