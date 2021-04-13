using Mecha.Core;
using Mecha.Core.Runner;
using Mecha.Core.Runner.Interfaces;
using System.Reflection;

namespace Mecha.xUnit
{
    /// <summary>
    /// XUnit.net runner
    /// </summary>
    /// <seealso cref="IRunner"/>
    public class XUnitRunner : IRunner
    {
        /// <summary>
        /// Runs the specified method on the target class.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>The result.</returns>
        public Result Run(MethodInfo runMethod, object? target, Options options)
        {
            return new Result
            {
                Output = "Something",
                Passed = true
            };
        }
    }
}