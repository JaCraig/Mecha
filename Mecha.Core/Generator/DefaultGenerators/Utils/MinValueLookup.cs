using System;
using System.Collections.Generic;

namespace Mecha.Core.Generator.DefaultGenerators.Utils
{
    /// <summary>
    /// Min value lookup
    /// </summary>
    public static class MinValueLookup
    {
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
    }
}