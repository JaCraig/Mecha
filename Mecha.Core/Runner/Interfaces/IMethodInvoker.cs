using System.Reflection;

namespace Mecha.Core.Runner.Interfaces
{
    /// <summary>
    /// Method invoker interface
    /// </summary>
    public interface IMethodInvoker
    {
        /// <summary>
        /// The method information
        /// </summary>
        MethodInfo? Method { get; set; }

        /// <summary>
        /// Invokes the method specified.
        /// </summary>
        /// <param name="target">The target object. Is null if the method is static.</param>
        /// <param name="parameters">The parameters to pass to the method.</param>
        /// <returns>The resulting object.</returns>
        object? Invoke(object? target, object?[] parameters);
    }
}