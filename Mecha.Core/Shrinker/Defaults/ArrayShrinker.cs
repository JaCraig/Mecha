using Fast.Activator;
using Mecha.Core.Shrinker.Interfaces;
using System;

namespace Mecha.Core.Shrinker.Defaults
{
    /// <summary>
    /// Array shrinker
    /// </summary>
    /// <seealso cref="IShrinker"/>
    public class ArrayShrinker : IShrinker
    {
        /// <summary>
        /// Determines whether this instance can shrink.
        /// </summary>
        /// <param name="value"></param>
        /// <returns><c>true</c> if this instance can shrink; otherwise, <c>false</c>.</returns>
        public bool CanShrink(object? value) => value?.GetType().IsArray == true;

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object? Shrink(object? value)
        {
            if (value is null || !CanShrink(value))
                return value;
            var OriginalArray = (Array)value;
            if (OriginalArray.Length == 0)
                return value;
            var NewLength = 4 * OriginalArray.Length / 5;
            var ArrayInstance = (Array)FastActivator.CreateInstance(value.GetType(), [NewLength]);
            for (var x = 0; x < NewLength; ++x)
            {
                ArrayInstance.SetValue(OriginalArray.GetValue(x), x);
            }
            return ArrayInstance;
        }
    }
}