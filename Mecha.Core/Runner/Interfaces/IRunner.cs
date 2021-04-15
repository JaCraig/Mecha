using System.Reflection;
using System.Threading.Tasks;

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
        /// <param name="options">The options.</param>
        /// <returns>The result.</returns>
        Task<Result> RunAsync(MethodInfo runMethod, object? target, Options options);
    }
}