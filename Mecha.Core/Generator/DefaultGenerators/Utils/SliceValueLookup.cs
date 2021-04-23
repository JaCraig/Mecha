using System;
using System.Collections.Generic;

namespace Mecha.Core.Generator.DefaultGenerators.Utils
{
    /// <summary>
    /// Slice value lookup
    /// </summary>
    public static class SliceValueLookup
    {
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
    }
}