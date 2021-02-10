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

using Mecha.Core.Generator.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Boundary generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class BoundaryGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BoundaryGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public BoundaryGenerator(Mirage.Random random)
        {
            Random = random;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => int.MaxValue;

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        private Mirage.Random Random { get; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter)
        {
            return !parameter.HasDefaultValue
                && parameter.GetCustomAttribute<ValidationAttribute>() == null
                && (DefaultValueLookup.Max?.ContainsKey(parameter.ParameterType.GetHashCode()) ?? false);
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        public object Next(ParameterInfo parameter)
        {
            var Key = parameter.ParameterType.GetHashCode();
            return Random.Next<bool>() ? (DefaultValueLookup.Max?[Key] ?? false) : (DefaultValueLookup.Min?[Key] ?? false);
        }
    }
}