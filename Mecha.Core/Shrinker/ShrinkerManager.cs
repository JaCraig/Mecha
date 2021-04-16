using Mecha.Core.Shrinker.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Mecha.Core.Shrinker
{
    /// <summary>
    /// Shrinker manager
    /// </summary>
    public class ShrinkerManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShrinkerManager"/> class.
        /// </summary>
        /// <param name="shrinkers">The shrinkers.</param>
        public ShrinkerManager(IEnumerable<IShrinker> shrinkers)
        {
            CustomShrinkers = shrinkers.Where(x => x.GetType().Assembly != typeof(ShrinkerManager).Assembly).ToList();
            DefaultShrinkers = shrinkers.Where(x => x.GetType().Assembly == typeof(ShrinkerManager).Assembly).ToList();
        }

        /// <summary>
        /// Gets the shrinkers.
        /// </summary>
        /// <value>The shrinkers.</value>
        private IEnumerable<IShrinker> CustomShrinkers { get; }

        /// <summary>
        /// Gets the default shrinkers.
        /// </summary>
        /// <value>The default shrinkers.</value>
        private IEnumerable<IShrinker> DefaultShrinkers { get; }

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "shrunk" value</returns>
        public object? Shrink(object? value)
        {
            foreach (var Shrinker in CustomShrinkers)
            {
                if (Shrinker.CanShrink(value))
                    return Shrinker.Shrink(value);
            }
            foreach (var Shrinker in DefaultShrinkers)
            {
                if (Shrinker.CanShrink(value))
                    return Shrinker.Shrink(value);
            }
            return value;
        }
    }
}