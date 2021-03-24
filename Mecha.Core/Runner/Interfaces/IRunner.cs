using System.Reflection;

namespace Mecha.Core.Runner.Interfaces
{
    /// <summary>
    /// Test runner interface
    /// </summary>
    public interface IRunner
    {
        /// <summary>
        /// Runs the specified method on the target class.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <returns>The result.</returns>
        Result Run(MethodInfo runMethod, object? target);
    }
}