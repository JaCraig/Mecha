/*
Copyright 2021 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using BigBook;
using Mecha.Core.Configuration;
using Mecha.Core.Generator.Interfaces;
using Mirage;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Timers;

namespace Mecha.Core.Generator
{
    /// <summary>
    /// Generator manager
    /// </summary>
    public class GeneratorManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorManager"/> class.
        /// </summary>
        /// <param name="generators">The generators.</param>
        /// <param name="random">The random.</param>
        public GeneratorManager(IEnumerable<IGenerator> generators, Random random)
        {
            Generators = generators;
            Random = random;
        }

        /// <summary>
        /// Gets the generators.
        /// </summary>
        /// <value>The generators.</value>
        public IEnumerable<IGenerator> Generators { get; }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Random Random { get; }

        /// <summary>
        /// Generates the data.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="options">The options.</param>
        /// <param name="previousItems">The previous items.</param>
        /// <returns>The data.</returns>
        public object?[] GenerateData(MethodInfo method, Options options, List<object?[]> previousItems)
        {
            var Parameters = method.GetParameters();
            if (Parameters.Length == 0)
                return System.Array.Empty<object?>();
            var Data = new object?[Parameters.Length];
            var Finished = false;
            using var InternalTimer = new Timer(options.MaxDuration);
            InternalTimer.Elapsed += (sender, e) => Finished = true;
            InternalTimer?.Start();
            do
            {
                Data = Next(Parameters);
                if (previousItems.AddIfUnique(Same, Data))
                    return Data;
            }
            while (!Finished);
            InternalTimer?.Stop();
            return System.Array.Empty<object?>();
        }

        /// <summary>
        /// Generates the data.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="generatorOptions">The generator options.</param>
        /// <returns>An IEnumerable that generates data on each pull.</returns>
        public IEnumerable<object?[]> GenerateData(MethodInfo method, GeneratorOptions generatorOptions, List<object?[]> previousItems)
        {
            var Count = generatorOptions.MaxCount;
            if (Count <= 0)
                yield break;
            var Parameters = method.GetParameters();
            if (Parameters.Length == 0)
                Count = 1;
            var Data = new object?[Parameters.Length];
            var Finished = false;
            using var InternalTimer = new Timer(generatorOptions.MaxDuration);
            InternalTimer.Elapsed += (sender, e) => Finished = true;
            InternalTimer?.Start();
            do
            {
                Data = Next(Parameters);
                if (previousItems.AddIfUnique(Same, Data))
                {
                    yield return Data;
                    --Count;
                    if (Count <= 0 || Finished)
                        break;
                }
            }
            while (!Finished);
            InternalTimer?.Stop();
        }

        /// <summary>
        /// Gets the next set of parameter values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The values.</returns>
        private object?[] Next(ParameterInfo[] parameters)
        {
            parameters ??= System.Array.Empty<ParameterInfo>();
            object?[] Data = new object?[parameters.Length];
            IGenerator[] LocalGenerators = Generators.ToArray();
            for (int i = 0, maxLength = parameters.Length; i < maxLength; i++)
            {
                var Parameter = parameters[i];
                LocalGenerators = Random.Shuffle(LocalGenerators).ToArray();
                Data[i] = System.Array
                    .Find(LocalGenerators, y => y.CanGenerate(Parameter))?
                    .Next(Parameter);
            }
            return Data;
        }

        /// <summary>
        /// Determines if the 2 arrays are the same.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>True if they are, false otherwise.</returns>
        private bool Same(object?[] value1, object?[] value2)
        {
            if (value1 is null || value2 is null)
                return false;
            if (value1.Length != value2.Length)
                return false;
            for (int x = 0; x < value1.Length; ++x)
            {
                var Value1 = JsonSerializer.Serialize(value1[x]);
                var Value2 = JsonSerializer.Serialize(value2[x]);
                if (Value1 != Value2)
                    return false;
            }
            return true;
        }
    }
}