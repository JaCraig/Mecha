using Mecha.Core.ExtensionMethods;
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
            var MechaCoreAssembly = TypeCache<Mech>.Assembly;
            var TempShrinkers = shrinkers.Where(x => x.GetType().Assembly != MechaCoreAssembly).ToList();
            TempShrinkers.AddRange(shrinkers.Where(x => x.GetType().Assembly == MechaCoreAssembly));
            Shrinkers = TempShrinkers.ToArray();
        }

        /// <summary>
        /// Gets the shrinkers.
        /// </summary>
        /// <value>The shrinkers.</value>
        private IShrinker[] Shrinkers { get; }

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "shrunk" value</returns>
        public object? Shrink(object? value)
        {
            foreach (var Shrinker in Shrinkers)
            {
                if (Shrinker.CanShrink(value))
                    return Shrinker.Shrink(value);
            }
            return value;
        }
    }
}