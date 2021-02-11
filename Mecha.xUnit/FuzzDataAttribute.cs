using Mecha.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Mecha.xUnit
{
    /// <summary>
    /// Data generator class used in theory methods.
    /// </summary>
    /// <seealso cref="DataAttribute"/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class FuzzDataAttribute : DataAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FountainDataAttribute"/> class.
        /// </summary>
        /// <param name="count">The number of items to generate.</param>
        /// <param name="maxDuration">The duration in ms. (overrides the count)</param>
        public FuzzDataAttribute(int count, int maxDuration = int.MaxValue)
        {
            Count = count;
            MaxDuration = maxDuration;
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public int MaxDuration { get; }

        /// <summary>
        /// Returns the data to be used to test the theory.
        /// </summary>
        /// <param name="testMethod">The method that is being tested</param>
        /// <returns>
        /// One or more sets of theory data. Each invocation of the test method is represented by a
        /// single object array.
        /// </returns>
        public override IEnumerable<object?[]> GetData(MethodInfo testMethod)
        {
            var Manager = Check.Default;
            if (Manager is null)
                throw new NullReferenceException($"{nameof(Manager)} is null");
            return Manager.Fuzz(testMethod, MaxDuration, Count);
        }
    }
}