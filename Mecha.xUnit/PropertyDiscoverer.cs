using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Mecha.xUnit
{
    /// <summary>
    /// Property discoverer
    /// </summary>
    /// <seealso cref="IXunitTestCaseDiscoverer"/>
    public class PropertyDiscoverer : IXunitTestCaseDiscoverer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDiscoverer"/> class.
        /// </summary>
        public PropertyDiscoverer()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyDiscoverer"/> class.
        /// </summary>
        /// <param name="messageSink">The message sink.</param>
        public PropertyDiscoverer(IMessageSink? messageSink)
        {
            MessageSink = messageSink;
        }

        /// <summary>
        /// Gets the message sink.
        /// </summary>
        /// <value>The message sink.</value>
        public IMessageSink? MessageSink { get; }

        /// <summary>
        /// Discover test cases from a test method.
        /// </summary>
        /// <param name="discoveryOptions">The discovery options to be used.</param>
        /// <param name="testMethod">The test method the test cases belong to.</param>
        /// <param name="factAttribute">The fact attribute attached to the test method.</param>
        /// <returns>Returns zero or more test cases represented by the test method.</returns>
        public IEnumerable<IXunitTestCase> Discover(ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo factAttribute)
        {
            return new IXunitTestCase[] { new PropertyTestCase(MessageSink, discoveryOptions.MethodDisplayOrDefault(), discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod) };
        }
    }
}