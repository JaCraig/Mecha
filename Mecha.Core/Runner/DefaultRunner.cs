using Mecha.Core.Runner.Interfaces;
using System;
using System.Reflection;

namespace Mecha.Core.Runner
{
    /// <summary>
    /// Default runner
    /// </summary>
    /// <seealso cref="IRunner"/>
    public class DefaultRunner : IRunner
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
            return new Result()
            {
                Output = "Runner not found",
                Passed = false,
                Exception = new Exception("Runner not found")
            };
        }
    }
}