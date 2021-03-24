using System;

namespace Mecha.Core.Runner
{
    /// <summary>
    /// Result data holder
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the execution time.
        /// </summary>
        /// <value>The execution time.</value>
        public decimal ExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets the output.
        /// </summary>
        /// <value>The output.</value>
        public string Output { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Result"/> is passed.
        /// </summary>
        /// <value><c>true</c> if passed; otherwise, <c>false</c>.</value>
        public bool Passed { get; set; }
    }
}