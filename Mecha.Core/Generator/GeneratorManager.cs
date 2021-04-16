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
using Mecha.Core.ExtensionMethods;
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
            Generators = generators.OrderBy(x => x.Order).ToArray();
            Random = random;
        }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Random Random { get; }

        /// <summary>
        /// Gets the generators.
        /// </summary>
        /// <value>The generators.</value>
        private IGenerator[] Generators { get; }

        /// <summary>
        /// Generates the parameter values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="options">The options.</param>
        /// <returns>The parameter values</returns>
        public ParameterValues[] GenerateParameterValues(ParameterInfo[] parameters, Options options)
        {
            parameters ??= System.Array.Empty<ParameterInfo>();
            var ReturnValue = new ParameterValues[parameters.Length];
            for (var x = 0; x < parameters.Length; ++x)
            {
                var CurrentParameter = ReturnValue[x] = new ParameterValues(parameters[x]);
                bool Finished = false;
                using var InternalTimer = new Timer(options.MaxDuration);
                InternalTimer.Elapsed += (sender, e) => Finished = true;
                InternalTimer.Start();
                int Index = 0;
                while (!Finished)
                {
                    var Generator = Generators[Index];
                    Index = (Index + 1) % Generators.Length;
                    if (!Generator.CanGenerate(CurrentParameter.Parameter))
                        continue;
                    var Min = CurrentParameter.GeneratedValues.Count > 0 ? Random.Next(CurrentParameter.GeneratedValues) : null;
                    var Max = CurrentParameter.GeneratedValues.Count > 0 ? Random.Next(CurrentParameter.GeneratedValues) : null;
                    var Data = Generator.Next(CurrentParameter.Parameter, Min, Max);
                    CurrentParameter.GeneratedValues.AddIfUnique(Same, Data);
                    if (CurrentParameter.GeneratedValues.Count >= options.GenerationCount)
                        break;
                }
                InternalTimer.Stop();
            }
            return ReturnValue;
        }

        /// <summary>
        /// Determines if the 2 arrays are the same.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>True if they are, false otherwise.</returns>
        private bool Same(object? value1, object? value2)
        {
            if (value1.IsInfinite() && value2.IsInfinite())
                return true;
            if (value1.IsInfinite() || value2.IsInfinite())
                return false;
            return (value1 is null && value2 is null)
                || (!(value1 is null)
                    && !(value2 is null)
                    && JsonSerializer.Serialize(value1) == JsonSerializer.Serialize(value2));
        }
    }
}