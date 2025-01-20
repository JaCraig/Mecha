using System;

namespace Mecha.Core.Generator.DefaultGenerators.Utils
{
    /// <summary>
    /// Basic types lookup
    /// </summary>
    public static class BasicTypesLookup
    {
        /// <summary>
        /// The types
        /// </summary>
        public static Type[] Types =
        [
            typeof(DummyClass),
            typeof(long),
            typeof(ulong),
            typeof(double),
            typeof(decimal),
            typeof(string),
            typeof(Guid),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan)
        ];

        /// <summary>
        /// Dummy class
        /// </summary>
        public class DummyClass
        {
        }
    }
}