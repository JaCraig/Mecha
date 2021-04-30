using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests.BaseClasses
{
    /// <summary>
    /// Test base class
    /// </summary>
    /// <typeparam name="TTestObject">The type of the test object.</typeparam>
    public abstract class TestBaseClass<TTestObject> : TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            ObjectType = typeof(TTestObject);
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type ObjectType { get; }

        /// <summary>
        /// Gets or sets the test object.
        /// </summary>
        /// <value>The test object.</value>
        protected TTestObject? TestObject { get; set; }

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakObject()
        {
            return Mech.BreakAsync(TestObject);
        }
    }

    /// <summary>
    /// Test base class
    /// </summary>
    public abstract class TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            _ = Mech.Default;
            TestMethodInfo = Array.Find(GetType().GetMethods(), x => x.Name == "TestMethod");
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected abstract Type ObjectType { get; }

        /// <summary>
        /// Gets the test method information.
        /// </summary>
        /// <value>The test method information.</value>
        protected MethodInfo? TestMethodInfo { get; }

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakType()
        {
            return Mech.BreakAsync(ObjectType);
        }

        /// <summary>
        /// Tests the method.
        /// </summary>
        /// <param name="val1">The val1.</param>
        /// <param name="val2">The val2.</param>
        public void TestMethod(int val1, int val2)
        {
        }
    }
}