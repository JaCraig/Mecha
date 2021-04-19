using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Utils extensions
    /// </summary>
    public static class UtilsExtensions
    {
        /// <summary>
        /// Determines whether the specified value is infinite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is infinite; otherwise, <c>false</c>.</returns>
        public static bool IsInfinite(this object? value)
        {
            if (value is null)
                return false;
            var ValueType = value.GetType();
            if (ValueType == typeof(float) || ValueType == typeof(float?))
                return !float.IsFinite((float)value);
            if (ValueType == typeof(double) || ValueType == typeof(double?))
                return !double.IsFinite((double)value);
            return false;
        }

        /// <summary>
        /// Timeouts the after.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task">The task.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        /// <exception cref="OperationCanceledException"></exception>
        public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout)
        {
            using (var Source = new CancellationTokenSource())
            {
                var DelayTask = Task.Delay(timeout, Source.Token);

                var ResultTask = await Task.WhenAny(task, DelayTask).ConfigureAwait(false);
                if (ResultTask == DelayTask)
                {
                    // Operation cancelled
                    throw new OperationCanceledException();
                }
                else
                {
                    // Cancel the timer task so that it does not fire
                    Source.Cancel();
                }

                return await task.ConfigureAwait(false);
            }
        }
    }
}