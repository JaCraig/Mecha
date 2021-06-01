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
            CustomMutators = mutators.Where(x => x.GetType().Assembly != typeof(MutatorManager).Assembly).ToList();
            DefaultMutators = mutators.Where(x => x.GetType().Assembly == typeof(MutatorManager).Assembly).ToList();
        }

        /// <summary>
        /// Gets the mutators.
        /// </summary>
        /// <value>The mutators.</value>
        private IEnumerable<IMutator> CustomMutators { get; }

        /// <summary>
        /// Gets the default mutators.
        /// </summary>
        /// <value>The default mutators.</value>
        private IEnumerable<IMutator> DefaultMutators { get; }

        /// <summary>
        /// Mutates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The mutated value.</returns>
        public object? Mutate(object? value)
        {
            foreach (var mutator in CustomMutators)
            {
                if (mutator.CanMutate(value))
                    return mutator.Mutate(value);
            }
            foreach (var mutator in DefaultMutators)
            {
                if (mutator.CanMutate(value))
                    return mutator.Mutate(value);
            }
            return value;
        }
    }
}