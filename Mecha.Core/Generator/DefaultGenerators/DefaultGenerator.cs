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

using Mecha.Core.ExtensionMethods;
using Mecha.Core.Generator.DefaultGenerators.Utils;
using Mecha.Core.Generator.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Default generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class DefaultGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public DefaultGenerator(Mirage.Random random)
        {
            RandomObj = random;
            MethodInfo[] Methods = RandomObj.GetType().GetMethods();
            GenericRandMethod = System.Array.Find(Methods, x => x.IsGenericMethod && x.GetParameters().Length == 2);
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => int.MaxValue;

        /// <summary>
        /// Gets the generic rand method.
        /// </summary>
        /// <value>The generic rand method.</value>
        private MethodInfo? GenericRandMethod { get; }

        /// <summary>
        /// Gets the random object.
        /// </summary>
        /// <value>The random object.</value>
        private Mirage.Random RandomObj { get; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo? parameter)
        {
            return parameter?.HasDefaultValue == false
                && !parameter.ParameterType.IsInterface
                && !parameter.ParameterType.IsAbstract
                && !parameter.ParameterType.IsSpecialType(out Type? _)
                && (parameter.ParameterType.IsValueType
                    || parameter.ParameterType.IsEnum
                    || parameter.ParameterType == typeof(string)
                    || parameter.ParameterType.GetConstructors().Any(x => x.GetParameters().Length == 0));
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The next object.</returns>
        public ParameterValue Next(ParameterInfo? parameter, object? min, object? max)
        {
            if (parameter is null || !CanGenerate(parameter))
                return new ParameterValue("Default Generator", null);
            var defaultValue = min;
            if (min == max)
            {
                max = min = null;
            }
            min = FixMinValue(min, parameter);
            max = FixMaxValue(max, parameter);
            if (min is not null
                && max is not null
                && min.GetType() == max.GetType()
                && min is IComparable MinValue
                && max is IComparable MaxValue
                && MinValue.CompareTo(MaxValue) > 0)
            {
                (max, min) = (min, max);
            }

            try
            {
                MethodInfo? GenericMethod = GenericRandMethod?.MakeGenericMethod(parameter.ParameterType);
                return new ParameterValue("Default Generator", GetValue(parameter, min, max, GenericMethod));
            }
            catch (Exception) { return new ParameterValue("Default Generator", defaultValue); }
        }

        /// <summary>
        /// Fixes the maximum value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private static object? FixMaxValue(object? value, ParameterInfo parameter)
        {
            var Key = parameter.ParameterType.GetHashCode();
            RangeAttribute? Range = parameter.GetCustomAttribute<RangeAttribute>();
            return value is not null || !(MaxValueLookup.Max?.ContainsKey(Key) ?? false)
                ? value
                : Range?.Maximum ?? MaxValueLookup.Max?[Key] ?? false;
        }

        /// <summary>
        /// Fixes the minimum value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private static object? FixMinValue(object? value, ParameterInfo parameter)
        {
            var Key = parameter.ParameterType.GetHashCode();
            RangeAttribute? Range = parameter.GetCustomAttribute<RangeAttribute>();
            return value is not null || !(MinValueLookup.Min?.ContainsKey(Key) ?? false)
                ? value
                : Range?.Minimum ?? MinValueLookup.Min?[Key] ?? false;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="genericMethod">The generic method.</param>
        /// <returns></returns>
        private object? GetValue(ParameterInfo parameter, object? min, object? max, MethodInfo? genericMethod)
        {
            return parameter.ParameterType.IsPrimitive
                    ? genericMethod?.Invoke(RandomObj, [min, max])
                    : RandomObj.Next(parameter.ParameterType);
        }
    }
}