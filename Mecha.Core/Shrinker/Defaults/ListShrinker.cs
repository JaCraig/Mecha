using Fast.Activator;
using Mecha.Core.Shrinker.Interfaces;
using System.Collections;

namespace Mecha.Core.Shrinker.Defaults
{
    /// <summary>
    /// List shrinker
    /// </summary>
    /// <seealso cref="IShrinker"/>
    public class ListShrinker : IShrinker
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
            System.Type? ValueType = value?.GetType();

            return typeof(IList).IsAssignableFrom(ValueType) && ValueType?.IsArray == false;
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
            var Val = (IList)value;
            if (Val.Count == 0)
                return value;
            Val = CopyList(Val);
            if (Val.Count > 10)
            {
                return RemoveItems(Val, Val.Count / 5);
            }
            Val.RemoveAt(Val.Count - 1);
            return Val;
        }

        /// <summary>
        /// Copies the list.
        /// </summary>
        /// <param name="Val">The value.</param>
        /// <returns></returns>
        private static IList CopyList(IList Val)
        {
            var NewVal = (IList)FastActivator.CreateInstance(Val.GetType());
            foreach (var Item in Val)
                _ = NewVal.Add(Item);
            return NewVal;
        }

        /// <summary>
        /// Removes the items.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private static IList RemoveItems(IList val, int count)
        {
            for (var x = 0; x < count; ++x)
                val.RemoveAt(val.Count - 1);
            return val;
        }
    }
}