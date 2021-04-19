using Fast.Activator;
using Mecha.Core.Shrinker.Interfaces;
using System.Collections;

namespace Mecha.Core.Shrinker.Defaults
{
    /// <summary>
    /// List shrinker
    /// </summary>
    /// <seealso cref="IShrinker"/>
    public class DictionaryShrinker : IShrinker
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

            return typeof(IDictionary).IsAssignableFrom(ValueType);
        }

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public object? Shrink(object? value)
        {
            if (value is null)
                return value;
            var Val = (IDictionary)value;
            if (Val.Count == 0)
                return value;
            Val = CopyDictionary(Val);
            var Keys = Val.Keys;
            if (Keys.Count > 10)
            {
                return RemoveItems(Val, Keys, Keys.Count / 5);
            }
            return RemoveItems(Val, Keys, 1);
        }

        /// <summary>
        /// Copies the list.
        /// </summary>
        /// <param name="Val">The value.</param>
        /// <returns></returns>
        private static IDictionary CopyDictionary(IDictionary Val)
        {
            IDictionary NewVal = (IDictionary)FastActivator.CreateInstance(Val.GetType());
            foreach (var Item in Val.Keys)
                NewVal.Add(Item, Val[Item]);
            return NewVal;
        }

        /// <summary>
        /// Removes the items.
        /// </summary>
        /// <param name="Val">The value.</param>
        /// <param name="Keys">The keys.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private static IDictionary RemoveItems(IDictionary Val, ICollection Keys, int count)
        {
            foreach (var Key in Keys)
            {
                --count;
                Val.Remove(Key);

                if (count <= 0)
                    break;
            }

            return Val;
        }
    }
}