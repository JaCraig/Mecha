using Mecha.Core.Configuration;
using Mecha.Core.Datasources;
using Mecha.Core.Generator;
using Mecha.Core.Runner;
using System;
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
        public Check(GeneratorManager generatorManager, DataManager dataManager)
        {
            DataManager = dataManager;
            GeneratorManager = generatorManager;
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
            foreach (var Data in GeneratorManager.GenerateData(testMethod, new GeneratorOptions { MaxCount = count, MaxDuration = maxDuration }))
            {
                DataManager.Save(testMethod, Data);
                yield return Data;
            }
        }

        public void Run(MethodInfo runMethod, object? target, out Result Result)
        {
            throw new NotImplementedException();
        }
    }
}