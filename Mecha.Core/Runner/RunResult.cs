using BigBook;
using System;
using System.Collections;
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

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            if (Method is null)
                return "";
            var ReturnVal = "";
            var ShrinkText = "";
            var ExceptionText = "";
            if (Method.ReturnType != typeof(void))
                ReturnVal = $" => {ReturnedValue}";
            if (!(Exception is null))
            {
                ShrinkText = $"\nNumber of shrinks: {ShrinkCount}";
                ExceptionText = $"\nException: {Exception}";
            }
            return $"{Method.Name} ({ParametersUsed.ToString(x => GetValue(x), ", ")}){ReturnVal}{ShrinkText}\nElapsed time: {ElapsedTime}{ExceptionText}";
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private string GetValue(object? value)
        {
            if (value is null)
                return "null";
            if (value is string StringValue)
                return StringValue;
            if (value is IDictionary IDictionaryValue)
            {
                var ReturnValue = "[ ";
                var Seperator = "";
                foreach (var Key in IDictionaryValue.Keys)
                {
                    ReturnValue += Seperator + "{ " + GetValue(Key) + ": " + GetValue(IDictionaryValue[Key]) + " }";
                    Seperator = ", ";
                }
                return ReturnValue + " ]";
            }
            if (value is IEnumerable IEnumerableValue)
            {
                var ReturnValue = "[ ";
                var Seperator = "";
                foreach (var Value in IEnumerableValue)
                {
                    ReturnValue += Seperator + GetValue(Value);
                    Seperator = ", ";
                }
                return ReturnValue + " ]";
            }
            return value.ToString();
        }
    }
}