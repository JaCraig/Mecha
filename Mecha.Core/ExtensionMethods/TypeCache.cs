using System.Reflection;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Type data cache
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    internal static class TypeCache<TClass>
    {
        /// <summary>
        /// Gets the assembly.
        /// </summary>
        /// <value>The assembly.</value>
        public static Assembly Assembly { get; } = typeof(TClass).Assembly;
    }
}