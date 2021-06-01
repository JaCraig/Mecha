namespace Mecha.Core.Mutator.Interfaces
{
    /// <summary>
    /// Mutator interface
    /// </summary>
    public interface IMutator
    {
        /// <summary>
        /// Determines whether this instance can mutate the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// <c>true</c> if this instance can mutate the specified value; otherwise, <c>false</c>.
        /// </returns>
        bool CanMutate(object? value);

        /// <summary>
        /// Mutates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The mutated value.</returns>
        object? Mutate(object? value);
    }
}