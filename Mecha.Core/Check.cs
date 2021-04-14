using Mecha.Core.Configuration;
using Mecha.Core.Datasources;
using Mecha.Core.Generator;
using Mecha.Core.Runner;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;

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
        public Check(GeneratorManager generatorManager, DataManager dataManager, TestRunnerManager testRunnerManager)
        {
            DataManager = dataManager;
            GeneratorManager = generatorManager;
            TestRunnerManager = testRunnerManager;
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
        /// Gets the test runner manager.
        /// </summary>
        /// <value>The test runner manager.</value>
        public TestRunnerManager TestRunnerManager { get; }

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// Generates data based on the method.
        /// </summary>
        /// <param name="testMethod">The test method.</param>
        /// <param name="maxDuration">The maximum duration.</param>
        /// <param name="count">The count.</param>
        /// <returns>An instance of data that can be used by the method.</returns>
        public IEnumerable<object?[]> Fuzz(MethodInfo testMethod, int maxDuration, int count)
        {
            if (count <= 0)
                yield break;
            var PreviousData = DataManager.Read(testMethod);
            for (int x = 0, PreviousDataCount = PreviousData.Count; x < PreviousDataCount; ++x)
            {
                if (count <= 0)
                    break;
                yield return PreviousData[x];
                --count;
            }
            foreach (var Data in GeneratorManager.GenerateData(testMethod, new GeneratorOptions { MaxCount = count, MaxDuration = maxDuration }, PreviousData))
            {
                DataManager.Save(testMethod, Data);
                yield return Data;
            }
        }

        /// <summary>
        /// Runs the specified run method.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <param name="Result">The result.</param>
        public void Run(MethodInfo runMethod, object? target, Options? options, out Result Result)
        {
            Result = TestRunnerManager.Run(runMethod, target, options ?? Options.Default);
        }
    }
}