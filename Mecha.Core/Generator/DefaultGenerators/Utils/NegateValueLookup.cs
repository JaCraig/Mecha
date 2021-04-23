using System;
using System.Collections.Generic;

namespace Mecha.Core.Generator.DefaultGenerators.Utils
{
    /// <summary>
    /// Negate value lookup
    /// </summary>
    public static class NegateValueLookup
    {
        /// <summary>
        /// Gets the negate.
        /// </summary>
        /// <value>The negate.</value>
        public static Dictionary<int, Func<object, object>>? Negate { get; } = new Dictionary<int, Func<object, object>>
        {
            [typeof(byte).GetHashCode()] = (value) => byte.MaxValue - Convert.ToByte(value),
            [typeof(sbyte).GetHashCode()] = (value) => sbyte.MaxValue - Convert.ToSByte(value),
            [typeof(short).GetHashCode()] = (value) => -(short)value,
            [typeof(int).GetHashCode()] = (value) => -(int)value,
            [typeof(long).GetHashCode()] = (value) => -(long)value,
            [typeof(ushort).GetHashCode()] = (value) => ushort.MaxValue - (ushort)value,
            [typeof(uint).GetHashCode()] = (value) => uint.MaxValue - (uint)value,
            [typeof(double).GetHashCode()] = (value) => -(double)value,
            [typeof(float).GetHashCode()] = (value) => -(float)value,
            [typeof(decimal).GetHashCode()] = (value) => -(decimal)value,
            [typeof(bool).GetHashCode()] = (value) => !(bool)value,
            [typeof(char).GetHashCode()] = (value) => char.MaxValue - Convert.ToChar(value),

            [typeof(byte?).GetHashCode()] = (value) => byte.MaxValue - Convert.ToByte(value ?? 0),
            [typeof(sbyte?).GetHashCode()] = (value) => sbyte.MaxValue - Convert.ToSByte(value ?? 0),
            [typeof(short?).GetHashCode()] = (value) => -(short)(value ?? 0),
            [typeof(int?).GetHashCode()] = (value) => -(int)(value ?? 0),
            [typeof(long?).GetHashCode()] = (value) => -(long)(value ?? 0),
            [typeof(ushort?).GetHashCode()] = (value) => ushort.MaxValue - (ushort)(value ?? 0),
            [typeof(uint?).GetHashCode()] = (value) => uint.MaxValue - (uint)(value ?? 0),
            [typeof(double?).GetHashCode()] = (value) => -(double)(value ?? 0),
            [typeof(float?).GetHashCode()] = (value) => -(float)(value ?? 0),
            [typeof(decimal?).GetHashCode()] = (value) => -(decimal)(value ?? 0),
            [typeof(bool?).GetHashCode()] = (value) => !(bool)(value ?? 0),
            [typeof(char?).GetHashCode()] = (value) => char.MaxValue - Convert.ToChar(value ?? 0),
        };
    }
}