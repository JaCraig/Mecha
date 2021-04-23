using System;
using System.Collections.Generic;

namespace Mecha.Core.Generator.DefaultGenerators.Utils
{
    /// <summary>
    /// Max value lookup
    /// </summary>
    public static class MaxValueLookup
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
    }
}