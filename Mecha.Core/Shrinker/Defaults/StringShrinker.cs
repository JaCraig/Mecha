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
        public bool CanShrink(object? value) => value?.GetType() == typeof(string);

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object? Shrink(object? value)
        {
            if (!CanShrink(value))
                return value;
            var StringVal = (string?)value;
            return string.IsNullOrEmpty(StringVal) ? StringVal : (object)StringVal.Remove(StringVal.Length - 1, 1);
        }
    }
}