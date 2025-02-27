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
using Mecha.Core.ExtensionMethods;
using Mecha.Core.Generator.DefaultGenerators.Utils;
using Mecha.Core.Generator.Interfaces;
using System.Diagnostics.CodeAnalysis;
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
        public int Order => int.MinValue;

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo? parameter) => (!parameter?.HasDefaultValue ?? false)
            && (!parameter?.ParameterType.IsSpecialType(out System.Type? _) ?? false);

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The next object.</returns>
        public ParameterValue? Next(ParameterInfo? parameter, object? min, object? max)
        {
            if (parameter is null || !CanGenerate(parameter))
                return new ParameterValue("DefaultValue Generator", null);
            DisallowNullAttribute? NotNullable = parameter.GetCustomAttribute<DisallowNullAttribute>();
            System.Type? ResultType = parameter.ParameterType;
            if (!ResultType.IsValueType && NotNullable is null)
                return new("DefaultValue Generator", null);
            if (ResultType.IsSpecialType(out System.Type? _))
                ResultType = ResultType.GetUnderlyingArrayType();
            if (ResultType is null)
                return new("DefaultValue Generator", null);
            try
            {
                return new ParameterValue("DefaultValue Generator", DefaultValueLookup.Values.TryGetValue(ResultType.GetHashCode(), out var ReturnValue)
                    ? ReturnValue
                    : FastActivator.CreateInstance(ResultType));
            }
            catch
            {
                return new ParameterValue("DefaultValue Generator", null);
            }
        }
    }
}