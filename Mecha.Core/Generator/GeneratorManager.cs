﻿/*
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

using Mecha.Core.Generator.Interfaces;
using Mirage;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;

namespace Mecha.Core.Generator
{
    /// <summary>
    /// Generator manager
    /// </summary>
    /// <remarks>Initializes a new instance of the <see cref="GeneratorManager"/> class.</remarks>
    /// <param name="generators">The generators.</param>
    /// <param name="random">The random.</param>
    public class GeneratorManager(IEnumerable<IGenerator> generators, Random random)
    {
        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Random Random { get; } = random;

        /// <summary>
        /// Gets the generators.
        /// </summary>
        /// <value>The generators.</value>
        private IGenerator[] Generators { get; } = [.. generators.OrderBy(x => x.Order)];

        /// <summary>
        /// Generates the parameter values.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="options">The options.</param>
        /// <returns>The parameter values</returns>
        public ParameterValues[] GenerateParameterValues(ParameterInfo[] parameters, Options? options)
        {
            options = options.Initialize();
            parameters ??= [];
            var ReturnValue = new ParameterValues[parameters.Length];
            for (var X = 0; X < parameters.Length; ++X)
            {
                ParameterValues CurrentParameter = ReturnValue[X] = new ParameterValues(parameters[X]);
                var Finished = false;
                using var InternalTimer = new Timer(options.MaxDuration);
                InternalTimer.Elapsed += (sender, e) => Finished = true;
                InternalTimer.Start();
                var Index = 0;
                while (!Finished)
                {
                    IGenerator Generator = Generators[Index];
                    Index = (Index + 1) % Generators.Length;
                    if (!Generator.CanGenerate(CurrentParameter.Parameter))
                        continue;
                    var Min = CurrentParameter.GeneratedValues.Count > 0 ? Random.Next(CurrentParameter.GeneratedValues).Value : null;
                    var Max = CurrentParameter.GeneratedValues.Count > 0 ? Random.Next(CurrentParameter.GeneratedValues).Value : null;
                    CurrentParameter.AddValue(Generator.Next(CurrentParameter.Parameter, Min, Max));
                    if (CurrentParameter.GeneratedValues.Count >= options.GenerationCount)
                        break;
                }
                InternalTimer.Stop();
            }
            return ReturnValue;
        }
    }
}