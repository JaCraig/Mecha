using System;
using Xunit;
using Xunit.Sdk;

namespace Mecha.xUnit
{
    /// <summary>
    /// Property attribute
    /// </summary>
    /// <seealso cref="Xunit.FactAttribute"/>
    [XunitTestCaseDiscoverer("Mecha.xUnit.PropertyDiscoverer", "Mecha.xUnit")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyAttribute : FactAttribute
    {
        /// <summary>
        /// Gets the number of randomly generated items to create.
        /// </summary>
        /// <value>The number of randomly generated items to create.</value>
        public int GenerationCount { get; set; } = 10;

        /// <summary>
        /// Gets the max duration to run the tests for.
        /// </summary>
        /// <value>The max duration to run the tests for.</value>
        public int MaxDuration { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the maximum shrink count.
        /// </summary>
        /// <value>The maximum shrink count.</value>
        public int MaxShrinkCount { get; set; } = 10;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PropertyAttribute"/> is verbose.
        /// </summary>
        /// <value><c>true</c> if verbose; otherwise, <c>false</c>.</value>
        public bool Verbose { get; set; }
    }
}