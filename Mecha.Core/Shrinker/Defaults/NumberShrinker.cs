using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Generator.DefaultGenerators.Utils;
using Mecha.Core.Shrinker.Interfaces;

namespace Mecha.Core.Shrinker.Defaults
{
    /// <summary>
    /// Number shrinker
    /// </summary>
    /// <seealso cref="IShrinker"/>
    public class NumberShrinker : IShrinker
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

            return SliceValueLookup.Slice?.ContainsKey(value.GetType().GetHashCode()) ?? false;
        }

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object? Shrink(object? value)
        {
            if (value is null || !CanShrink(value))
                return value;
            var ValueType = value.GetType().GetHashCode();
            return SliceValueLookup.Slice?[ValueType](DefaultValueLookup.Values?[ValueType] ?? value, value);
        }
    }
}