namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Utils extensions
    /// </summary>
    public static class UtilsExtensions
    {
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