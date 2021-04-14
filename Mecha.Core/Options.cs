namespace Mecha.Core
{
    /// <summary>
    /// Options
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static Options Default => new Options
        {
            GenerationCount = 10,
            MaxDuration = 60,
            Verbose = true,
            MaxShrinkCount = 10
        };

        /// <summary>
        /// Gets the number of generated items to create.
        /// </summary>
        /// <value>The number of generated items to create.</value>
        public int GenerationCount { get; set; }

        /// <summary>
        /// Gets the max duration to run the tests for.
        /// </summary>
        /// <value>The max duration to run the tests for.</value>
        public int MaxDuration { get; set; }

        /// <summary>
        /// Gets or sets the maximum shrink count.
        /// </summary>
        /// <value>The maximum shrink count.</value>
        public int MaxShrinkCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Options"/> is verbose.
        /// </summary>
        /// <value><c>true</c> if verbose; otherwise, <c>false</c>.</value>
        public bool Verbose { get; set; }
    }
}