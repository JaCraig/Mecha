using Mecha.Core.Mutator.Interfaces;
using System;

namespace Mecha.Core.Mutator.Defaults
{
    /// <summary>
    /// String mutator
    /// </summary>
    /// <seealso cref="IMutator"/>
    public class StringMutator : IMutator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringMutator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public StringMutator(Mirage.Random random)
        {
            Random = random;
        }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        private Mirage.Random Random { get; }

        /// <summary>
        /// Determines whether this instance can mutate the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if this instance can mutate the specified value; otherwise, <c>false</c>.
        /// </returns>
        public bool CanMutate(object? value) => value?.GetType() == typeof(string);

        /// <summary>
        /// Mutates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The mutated value.</returns>
        public object? Mutate(object? value)
        {
            if (!CanMutate(value))
                return value;
            var StringVal = (string?)value;
            if (string.IsNullOrEmpty(StringVal))
                return "\0";
            var Index = Random.Next(0, StringVal.Length - 1);
            return StringVal.Insert(Index, "\0");
        }
    }
}