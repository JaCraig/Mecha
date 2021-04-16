using Mecha.Core.Shrinker.Interfaces;

namespace Mecha.Core.Shrinker.Defaults
{
    /// <summary>
    /// String shrinker
    /// </summary>
    /// <seealso cref="IShrinker"/>
    public class StringShrinker : IShrinker
    {
        /// <summary>
        /// Determines whether this instance can shrink.
        /// </summary>
        /// <param name="value"></param>
        /// <returns><c>true</c> if this instance can shrink; otherwise, <c>false</c>.</returns>
        public bool CanShrink(object? value)
        {
            if (value is null)
                return false;
            var ValueType = value?.GetType();

            return ValueType == typeof(string);
        }

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object? Shrink(object? value)
        {
            var StringVal = (string)value;
            if (string.IsNullOrEmpty(StringVal))
                return StringVal;
            return StringVal.Remove(StringVal.Length - 1, 1);
        }
    }
}