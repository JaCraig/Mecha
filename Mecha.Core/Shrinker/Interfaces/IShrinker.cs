namespace Mecha.Core.Shrinker.Interfaces
{
    /// <summary>
    /// Shrinker interface
    /// </summary>
    public interface IShrinker
    {
        /// <summary>
        /// Determines whether this instance can shrink.
        /// </summary>
        /// <returns><c>true</c> if this instance can shrink; otherwise, <c>false</c>.</returns>
        bool CanShrink(object? value);

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The "shrunk" value.</returns>
        object? Shrink(object? value);
    }
}