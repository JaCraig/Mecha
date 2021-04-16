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
            var ValueType = value?.GetType();

            return typeof(IList).IsAssignableFrom(ValueType);
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
            var Val = (IList)value;
            if (Val.Count == 0)
                return value;
            Val = CopyList(Val);
            if (Val.Count > 50)
            {
                for (var x = 0; x < 10; ++x)
                    Val.RemoveAt(Val.Count - 1);
                return Val;
            }
            if (Val.Count > 25)
            {
                for (var x = 0; x < 5; ++x)
                    Val.RemoveAt(Val.Count - 1);
                return Val;
            }
            if (Val.Count > 10)
            {
                for (var x = 0; x < 2; ++x)
                    Val.RemoveAt(Val.Count - 1);
                return Val;
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
            IList NewVal = (IList)FastActivator.CreateInstance(Val.GetType());
            foreach (var Item in Val)
                NewVal.Add(Item);
            return NewVal;
        }
    }
}