using Mecha.Core.ExtensionMethods;
using Mecha.Core.Mutator.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Mecha.Core.Mutator
{
    /// <summary>
    /// Mutator manager
    /// </summary>
    public class MutatorManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutatorManager"/> class.
        /// </summary>
        /// <param name="mutators">The mutators.</param>
        public MutatorManager(IEnumerable<IMutator> mutators)
        {
            var MechaCoreAssembly = TypeCache<Mech>.Assembly;
            var TempMutators = mutators.Where(x => x.GetType().Assembly != MechaCoreAssembly).ToList();
            TempMutators.AddRange(mutators.Where(x => x.GetType().Assembly == MechaCoreAssembly));
            Mutators = TempMutators.ToArray();
        }

        /// <summary>
        /// Gets the mutators.
        /// </summary>
        /// <value>The mutators.</value>
        private IMutator[] Mutators { get; }

        /// <summary>
        /// Mutates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The mutated value.</returns>
        public object? Mutate(object? value)
        {
            foreach (var Mutator in Mutators)
            {
                if (Mutator.CanMutate(value))
                    return Mutator.Mutate(value);
            }
            return value;
        }
    }
}