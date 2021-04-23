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