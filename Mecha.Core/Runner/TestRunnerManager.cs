using Mecha.Core.Runner.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mecha.Core.Runner
{
    /// <summary>
    /// Test runner manager
    /// </summary>
    public class TestRunnerManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunnerManager"/> class.
        /// </summary>
        /// <param name="runners">The runners.</param>
        public TestRunnerManager(IEnumerable<IRunner> runners)
        {
            Runner = runners.FirstOrDefault(x => x.GetType() != typeof(DefaultRunner)) ?? runners.FirstOrDefault(x => x.GetType() == typeof(DefaultRunner));
        }

        /// <summary>
        /// Gets the runners.
        /// </summary>
        /// <value>The runners.</value>
        private IRunner Runner { get; }

        /// <summary>
        /// Runs the specified method information.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>Results</returns>
        public Task<Result> RunAsync(MethodInfo methodInfo, object? target, Options options)
        {
            return Runner.RunAsync(methodInfo, target, options);
        }
    }
}