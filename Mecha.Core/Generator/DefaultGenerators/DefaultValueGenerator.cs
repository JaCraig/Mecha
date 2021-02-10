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

using Fast.Activator;
using Mecha.Core.Generator.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Default value generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class DefaultValueGenerator : IGenerator
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => int.MaxValue;

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter)
        {
            return !parameter.HasDefaultValue && parameter.GetCustomAttribute<ValidationAttribute>() is null;
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        public object? Next(ParameterInfo parameter)
        {
            var ResultType = parameter.ParameterType;
            if (!ResultType.IsValueType)
                return null;
            var ResultHash = ResultType.GetHashCode();
            if (DefaultValueLookup.Values.TryGetValue(ResultHash, out var ReturnValue))
                return ReturnValue;
            return FastActivator.CreateInstance(ResultType);
        }
    }
}