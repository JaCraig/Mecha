using System;
using System.Collections.Generic;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Utils extensions
    /// </summary>
    public static class UtilsExtensions
    {
        /// <summary>
        /// The methods
        /// </summary>
        private static readonly Dictionary<Type, Func<object, bool>> Methods = new()
        {
            [typeof(float)] = value => !float.IsFinite((float)value),
            [typeof(float?)] = value => !float.IsFinite((float)value),
            [typeof(double)] = value => !double.IsFinite((double)value),
            [typeof(double?)] = value => !double.IsFinite((double)value),
        };

        /// <summary>
        /// Determines whether the specified value is infinite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is infinite; otherwise, <c>false</c>.</returns>
        public static bool IsInfinite(this object? value)
        {
            return value is not null
                && Methods.TryGetValue(value.GetType(), out Func<object, bool>? Result)
                && Result(value);
        }
    }
}