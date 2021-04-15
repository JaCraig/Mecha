using Mecha.Core.Datasources;
using Mecha.Core.Generator;
using Mecha.Core.Runner;
using Microsoft.Extensions.DependencyInjection;
using Mirage;
using System.Reflection;
using System.Threading.Tasks;

namespace Mecha.Core
{
    /// <summary>
    /// Main entry point for the system
    /// </summary>
    public class Check
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Check"/> class.
        /// </summary>
        /// <param name="generatorManager">The generator manager.</param>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="testRunnerManager">The test runner manager.</param>
        public Check(GeneratorManager generatorManager, DataManager dataManager, TestRunnerManager testRunnerManager, Random random)
        {
            DataManager = dataManager;
            GeneratorManager = generatorManager;
            TestRunnerManager = testRunnerManager;
            Random = random;
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static Check? Default
        {
            get
            {
                if (Canister.Builder.Bootstrapper is null)
                {
                    lock (LockObject)
                    {
                        if (Canister.Builder.Bootstrapper is null)
                        {
                            new ServiceCollection().AddCanisterModules(configure => configure.RegisterMecha());
                        }
                    }
                }
                return Canister.Builder.Bootstrapper?.Resolve<Check>();
            }
        }

        /// <summary>
        /// Gets the data manager.
        /// </summary>
        /// <value>The data manager.</value>
        public DataManager DataManager { get; }

        /// <summary>
        /// Gets the generator manager.
        /// </summary>
        /// <value>The generator manager.</value>
        public GeneratorManager GeneratorManager { get; }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Random Random { get; }

        /// <summary>
        /// Gets the test runner manager.
        /// </summary>
        /// <value>The test runner manager.</value>
        public TestRunnerManager TestRunnerManager { get; }

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// Runs the specified run method.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>The result</returns>
        public Task<Result> RunAsync(MethodInfo runMethod, object? target, Options? options)
        {
            return TestRunnerManager.RunAsync(runMethod, target, options ?? Options.Default);
        }
    }
}