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

using System;
using System.Collections.Generic;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Default value lookup
    /// </summary>
    public static class DefaultValueLookup
    {
        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>The maximum.</value>
        public static Dictionary<int, object>? Max { get; } = new Dictionary<int, object>
        {
            [typeof(byte).GetHashCode()] = byte.MaxValue,
            [typeof(sbyte).GetHashCode()] = sbyte.MaxValue,
            [typeof(short).GetHashCode()] = short.MaxValue,
            [typeof(int).GetHashCode()] = int.MaxValue,
            [typeof(long).GetHashCode()] = long.MaxValue,
            [typeof(ushort).GetHashCode()] = ushort.MaxValue,
            [typeof(uint).GetHashCode()] = uint.MaxValue,
            [typeof(ulong).GetHashCode()] = ulong.MaxValue,
            [typeof(double).GetHashCode()] = double.MaxValue,
            [typeof(float).GetHashCode()] = float.MaxValue,
            [typeof(decimal).GetHashCode()] = decimal.MaxValue,
            [typeof(bool).GetHashCode()] = true,
            [typeof(char).GetHashCode()] = char.MaxValue,

            [typeof(byte?).GetHashCode()] = byte.MaxValue,
            [typeof(sbyte?).GetHashCode()] = sbyte.MaxValue,
            [typeof(short?).GetHashCode()] = short.MaxValue,
            [typeof(int?).GetHashCode()] = int.MaxValue,
            [typeof(long?).GetHashCode()] = long.MaxValue,
            [typeof(ushort?).GetHashCode()] = ushort.MaxValue,
            [typeof(uint?).GetHashCode()] = uint.MaxValue,
            [typeof(ulong?).GetHashCode()] = ulong.MaxValue,
            [typeof(double?).GetHashCode()] = double.MaxValue,
            [typeof(float?).GetHashCode()] = float.MaxValue,
            [typeof(decimal?).GetHashCode()] = decimal.MaxValue,
            [typeof(bool?).GetHashCode()] = true,
            [typeof(char?).GetHashCode()] = char.MaxValue,

            [typeof(DateTime).GetHashCode()] = DateTime.MaxValue,
            [typeof(DateTimeOffset).GetHashCode()] = DateTimeOffset.MaxValue,
            [typeof(TimeSpan).GetHashCode()] = TimeSpan.MaxValue,
            [typeof(DateTime?).GetHashCode()] = DateTime.MaxValue,
            [typeof(DateTimeOffset?).GetHashCode()] = DateTimeOffset.MaxValue,
            [typeof(TimeSpan?).GetHashCode()] = TimeSpan.MaxValue,
        };

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public static Dictionary<int, object>? Min { get; } = new Dictionary<int, object>
        {
            [typeof(byte).GetHashCode()] = byte.MinValue,
            [typeof(sbyte).GetHashCode()] = sbyte.MinValue,
            [typeof(short).GetHashCode()] = short.MinValue,
            [typeof(int).GetHashCode()] = int.MinValue,
            [typeof(long).GetHashCode()] = long.MinValue,
            [typeof(ushort).GetHashCode()] = ushort.MinValue,
            [typeof(uint).GetHashCode()] = uint.MinValue,
            [typeof(ulong).GetHashCode()] = ulong.MinValue,
            [typeof(double).GetHashCode()] = double.MinValue,
            [typeof(float).GetHashCode()] = float.MinValue,
            [typeof(decimal).GetHashCode()] = decimal.MinValue,
            [typeof(bool).GetHashCode()] = false,
            [typeof(char).GetHashCode()] = char.MinValue,

            [typeof(byte?).GetHashCode()] = byte.MinValue,
            [typeof(sbyte?).GetHashCode()] = sbyte.MinValue,
            [typeof(short?).GetHashCode()] = short.MinValue,
            [typeof(int?).GetHashCode()] = int.MinValue,
            [typeof(long?).GetHashCode()] = long.MinValue,
            [typeof(ushort?).GetHashCode()] = ushort.MinValue,
            [typeof(uint?).GetHashCode()] = uint.MinValue,
            [typeof(ulong?).GetHashCode()] = ulong.MinValue,
            [typeof(double?).GetHashCode()] = double.MinValue,
            [typeof(float?).GetHashCode()] = float.MinValue,
            [typeof(decimal?).GetHashCode()] = decimal.MinValue,
            [typeof(bool?).GetHashCode()] = false,
            [typeof(char?).GetHashCode()] = char.MinValue,

            [typeof(DateTime).GetHashCode()] = DateTime.MinValue,
            [typeof(DateTimeOffset).GetHashCode()] = DateTimeOffset.MinValue,
            [typeof(TimeSpan).GetHashCode()] = TimeSpan.MinValue,
            [typeof(DateTime?).GetHashCode()] = DateTime.MinValue,
            [typeof(DateTimeOffset?).GetHashCode()] = DateTimeOffset.MinValue,
            [typeof(TimeSpan?).GetHashCode()] = TimeSpan.MinValue,
        };

        /// <summary>
        /// The values
        /// </summary>
        public static Dictionary<int, object?> Values { get; } = new Dictionary<int, object?>
        {
            [typeof(byte).GetHashCode()] = default(byte),
            [typeof(sbyte).GetHashCode()] = default(sbyte),
            [typeof(short).GetHashCode()] = default(short),
            [typeof(int).GetHashCode()] = default(int),
            [typeof(long).GetHashCode()] = default(long),
            [typeof(ushort).GetHashCode()] = default(ushort),
            [typeof(uint).GetHashCode()] = default(uint),
            [typeof(ulong).GetHashCode()] = default(ulong),
            [typeof(double).GetHashCode()] = default(double),
            [typeof(float).GetHashCode()] = default(float),
            [typeof(decimal).GetHashCode()] = default(decimal),
            [typeof(bool).GetHashCode()] = default(bool),
            [typeof(char).GetHashCode()] = default(char),

            [typeof(byte?).GetHashCode()] = default(byte?),
            [typeof(sbyte?).GetHashCode()] = default(sbyte?),
            [typeof(short?).GetHashCode()] = default(short?),
            [typeof(int?).GetHashCode()] = default(int?),
            [typeof(long?).GetHashCode()] = default(long?),
            [typeof(ushort?).GetHashCode()] = default(ushort?),
            [typeof(uint?).GetHashCode()] = default(uint?),
            [typeof(ulong?).GetHashCode()] = default(ulong?),
            [typeof(double?).GetHashCode()] = default(double?),
            [typeof(float?).GetHashCode()] = default(float?),
            [typeof(decimal?).GetHashCode()] = default(decimal?),
            [typeof(bool?).GetHashCode()] = default(bool?),
            [typeof(char?).GetHashCode()] = default(char?),

            [typeof(string).GetHashCode()] = default(string),
            [typeof(Guid).GetHashCode()] = default(Guid),
            [typeof(DateTime).GetHashCode()] = default(DateTime),
            [typeof(DateTimeOffset).GetHashCode()] = default(DateTimeOffset),
            [typeof(TimeSpan).GetHashCode()] = default(TimeSpan),
            [typeof(Guid?).GetHashCode()] = default(Guid?),
            [typeof(DateTime?).GetHashCode()] = default(DateTime?),
            [typeof(DateTimeOffset?).GetHashCode()] = default(DateTimeOffset?),
            [typeof(TimeSpan?).GetHashCode()] = default(TimeSpan?),
            [typeof(byte[]).GetHashCode()] = default(byte[]),
        };
    }
}