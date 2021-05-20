using System.Linq;
using System.Reflection;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Utils extensions
    /// </summary>
    public static class UtilsExtensions
    {
        /// <summary>
        /// Are the methods equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>True if they are, false otherwise.</returns>
        public static bool AreMethodsEqual(this MethodBase left, MethodBase right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;
            if (left.Equals(right))
                return true;
            try
            {
                var RightMethod = left.DeclaringType.GetMethod(right.Name, right.GetGenericArguments().Length, right.GetParameters().Select(p => p.ParameterType).ToArray());
                if (RightMethod is null)
                    return false;
                return left.Equals(RightMethod);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether the specified value is infinite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is infinite; otherwise, <c>false</c>.</returns>
        public static bool IsInfinite(this object? value)
        {
            if (value is null)
                return false;
            var ValueType = value.GetType();
            if (ValueType == typeof(float) || ValueType == typeof(float?))
                return !float.IsFinite((float)value);
            if (ValueType == typeof(double) || ValueType == typeof(double?))
                return !double.IsFinite((double)value);
            return false;
        }
    }
}