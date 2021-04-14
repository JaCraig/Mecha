using System;
using System.Reflection;

namespace Mecha.Core.Runner
{
    /// <summary>
    /// Individual run result
    /// </summary>
    public class RunResult
    {
        /// <summary>
        /// Gets or sets the elapsed time.
        /// </summary>
        /// <value>The elapsed time.</value>
        public decimal ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public MethodInfo? Method { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public ParameterInfo[] Parameters { get; set; } = Array.Empty<ParameterInfo>();

        /// <summary>
        /// Gets or sets the parameters used.
        /// </summary>
        /// <value>The parameters used.</value>
        public object?[] ParametersUsed { get; set; } = Array.Empty<object?>();

        /// <summary>
        /// Gets or sets the returned value.
        /// </summary>
        /// <value>The returned value.</value>
        public object? ReturnedValue { get; set; }

        /// <summary>
        /// Gets or sets the shrink count.
        /// </summary>
        /// <value>The shrink count.</value>
        public int ShrinkCount { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public object? Target { get; set; }
    }
}