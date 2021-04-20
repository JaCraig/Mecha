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
        /// Gets the negate.
        /// </summary>
        /// <value>The negate.</value>
        public static Dictionary<int, Func<object, object>>? Negate { get; } = new Dictionary<int, Func<object, object>>
        {
            [typeof(byte).GetHashCode()] = (value) => -(byte)value,
            [typeof(sbyte).GetHashCode()] = (value) => -(sbyte)value,
            [typeof(short).GetHashCode()] = (value) => -(short)value,
            [typeof(int).GetHashCode()] = (value) => -(int)value,
            [typeof(long).GetHashCode()] = (value) => -(long)value,
            [typeof(ushort).GetHashCode()] = (value) => -(ushort)value,
            [typeof(uint).GetHashCode()] = (value) => -(uint)value,
            [typeof(double).GetHashCode()] = (value) => -(double)value,
            [typeof(float).GetHashCode()] = (value) => -(float)value,
            [typeof(decimal).GetHashCode()] = (value) => -(decimal)value,
            [typeof(bool).GetHashCode()] = (value) => !(bool)value,
            [typeof(char).GetHashCode()] = (value) => -(char)value,

            [typeof(byte?).GetHashCode()] = (value) => -(byte)value,
            [typeof(sbyte?).GetHashCode()] = (value) => -(sbyte)value,
            [typeof(short?).GetHashCode()] = (value) => -(short)value,
            [typeof(int?).GetHashCode()] = (value) => -(int)value,
            [typeof(long?).GetHashCode()] = (value) => -(long)value,
            [typeof(ushort?).GetHashCode()] = (value) => -(ushort)value,
            [typeof(uint?).GetHashCode()] = (value) => -(uint)value,
            [typeof(double?).GetHashCode()] = (value) => -(double)value,
            [typeof(float?).GetHashCode()] = (value) => -(float)value,
            [typeof(decimal?).GetHashCode()] = (value) => -(decimal)value,
            [typeof(bool?).GetHashCode()] = (value) => !(bool)value,
            [typeof(char?).GetHashCode()] = (value) => -(char)value,
        };

        /// <summary>
        /// Slices the items and gets the mid point between the two values.
        /// </summary>
        /// <value>The mid point between the two values.</value>
        public static Dictionary<int, Func<object, object, object>>? Slice { get; } = new Dictionary<int, Func<object, object, object>>
        {
            [typeof(byte).GetHashCode()] = (value1, value2) => ((byte)value1 / 2) + ((byte)value2 / 2),
            [typeof(sbyte).GetHashCode()] = (value1, value2) => ((sbyte)value1 / 2) + ((sbyte)value2 / 2),
            [typeof(short).GetHashCode()] = (value1, value2) => ((short)value1 / 2) + ((short)value2 / 2),
            [typeof(int).GetHashCode()] = (value1, value2) => ((int)value1 / 2) + ((int)value2 / 2),
            [typeof(long).GetHashCode()] = (value1, value2) => ((long)value1 / 2) + ((long)value2 / 2),
            [typeof(ushort).GetHashCode()] = (value1, value2) => ((ushort)value1 / 2) + ((ushort)value2 / 2),
            [typeof(uint).GetHashCode()] = (value1, value2) => ((uint)value1 / 2) + ((uint)value2 / 2),
            [typeof(ulong).GetHashCode()] = (value1, value2) => ((ulong)value1 / 2) + ((ulong)value2 / 2),
            [typeof(double).GetHashCode()] = (value1, value2) => ((double)value1 / 2) + ((double)value2 / 2),
            [typeof(float).GetHashCode()] = (value1, value2) => ((float)value1 / 2) + ((float)value2 / 2),
            [typeof(decimal).GetHashCode()] = (value1, value2) => ((decimal)value1 / 2) + ((decimal)value2 / 2),
            [typeof(char).GetHashCode()] = (value1, value2) => ((char)value1 / 2) + ((char)value2 / 2),

            [typeof(byte?).GetHashCode()] = (value1, value2) => (((byte?)value1 ?? byte.MinValue) / 2) + (((byte?)value2 ?? byte.MaxValue) / 2),
            [typeof(sbyte?).GetHashCode()] = (value1, value2) => (((sbyte?)value1 ?? sbyte.MinValue) / 2) + (((sbyte?)value2 ?? sbyte.MaxValue) / 2),
            [typeof(short?).GetHashCode()] = (value1, value2) => (((short?)value1 ?? short.MinValue) / 2) + (((short?)value2 ?? short.MaxValue) / 2),
            [typeof(int?).GetHashCode()] = (value1, value2) => (((int?)value1 ?? int.MinValue) / 2) + (((int?)value2 ?? int.MaxValue) / 2),
            [typeof(long?).GetHashCode()] = (value1, value2) => (((long?)value1 ?? long.MinValue) / 2) + (((long?)value2 ?? long.MaxValue) / 2),
            [typeof(ushort?).GetHashCode()] = (value1, value2) => (((ushort?)value1 ?? ushort.MinValue) / 2) + (((ushort?)value2 ?? ushort.MaxValue) / 2),
            [typeof(uint?).GetHashCode()] = (value1, value2) => (((uint?)value1 ?? uint.MinValue) / 2) + (((uint?)value2 ?? uint.MaxValue) / 2),
            [typeof(ulong?).GetHashCode()] = (value1, value2) => (((ulong?)value1 ?? ulong.MinValue) / 2) + (((ulong?)value2 ?? ulong.MaxValue) / 2),
            [typeof(double?).GetHashCode()] = (value1, value2) => (((double?)value1 ?? double.MinValue) / 2) + (((double?)value2 ?? double.MaxValue) / 2),
            [typeof(float?).GetHashCode()] = (value1, value2) => (((float?)value1 ?? float.MinValue) / 2) + (((float?)value2 ?? float.MaxValue) / 2),
            [typeof(decimal?).GetHashCode()] = (value1, value2) => (((decimal?)value1 ?? decimal.MinValue) / 2) + (((decimal?)value2 ?? decimal.MaxValue) / 2),
            [typeof(char?).GetHashCode()] = (value1, value2) => (((char?)value1 ?? char.MinValue) / 2) + (((char?)value2 ?? char.MaxValue) / 2),

            [typeof(DateTime).GetHashCode()] = (value1, value2) => new DateTime((((DateTime)value1).Ticks / 2) + (((DateTime)value2).Ticks / 2)),
            [typeof(DateTimeOffset).GetHashCode()] = (value1, value2) => new DateTimeOffset(new DateTime((((DateTimeOffset)value1).Ticks / 2) + (((DateTimeOffset)value2).Ticks / 2))),
            [typeof(TimeSpan).GetHashCode()] = (value1, value2) => new TimeSpan((((TimeSpan)value1).Ticks / 2) + (((TimeSpan)value2).Ticks / 2)),
            [typeof(DateTime?).GetHashCode()] = (value1, value2) => new DateTime((((DateTime?)value1 ?? DateTime.MinValue).Ticks / 2) + (((DateTime?)value2 ?? DateTime.MaxValue).Ticks / 2)),
            [typeof(DateTimeOffset?).GetHashCode()] = (value1, value2) => new DateTimeOffset(new DateTime((((DateTimeOffset?)value1 ?? DateTimeOffset.MinValue).Ticks / 2) + (((DateTimeOffset?)value2 ?? DateTimeOffset.MaxValue).Ticks / 2))),
            [typeof(TimeSpan?).GetHashCode()] = (value1, value2) => new TimeSpan((((TimeSpan?)value1 ?? TimeSpan.MinValue).Ticks / 2) + (((TimeSpan?)value2 ?? TimeSpan.MaxValue).Ticks / 2)),
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