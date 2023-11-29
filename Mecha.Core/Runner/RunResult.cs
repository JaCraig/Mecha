using BigBook;
using Mecha.Core.Mutator;
using Mecha.Core.Shrinker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mecha.Core.Runner
{
    /// <summary>
    /// Individual run result
    /// </summary>
    public class RunResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunResult"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="parameterValues">The parameter values.</param>
        public RunResult(MethodInfo method, object? target, object?[] parameterValues)
        {
            Method = method;
            Target = target;
            ParameterInfo[] TempParameters = method.GetParameters();
            Parameters = new Parameter[TempParameters.Length];
            for (var X = 0; X < TempParameters.Length; ++X)
            {
                Parameters[X] = new Parameter(TempParameters[X], parameterValues[X]);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RunResult"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="shrinkCount">The shrink count.</param>
        /// <param name="mutationCount">The mutation count.</param>
        /// <param name="returnedValue">The returned value.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="elapsedTime">The elapsed time.</param>
        private RunResult(
            MethodInfo method,
            object? target,
            Parameter[] parameters,
            int shrinkCount,
            int mutationCount,
            object? returnedValue,
            Exception? exception,
            decimal elapsedTime)
        {
            Method = method;
            Target = target;
            Parameters = parameters.ToArray(x => x.Copy());
            ShrinkCount = shrinkCount;
            MutationCount = mutationCount;
            ReturnedValue = returnedValue;
            Exception = exception;
            ElapsedTime = elapsedTime;
        }

        /// <summary>
        /// Gets or sets the elapsed time.
        /// </summary>
        /// <value>The elapsed time.</value>
        public decimal ElapsedTime { get; private set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception? Exception { get; private set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public MethodInfo Method { get; }

        /// <summary>
        /// Gets the mutation count.
        /// </summary>
        /// <value>The mutation count.</value>
        public int MutationCount { get; private set; }

        /// <summary>
        /// Gets or sets the parameters used.
        /// </summary>
        /// <value>The parameters used.</value>
        public Parameter[] Parameters { get; }

        /// <summary>
        /// Gets or sets the returned value.
        /// </summary>
        /// <value>The returned value.</value>
        public object? ReturnedValue { get; private set; }

        /// <summary>
        /// Gets or sets the shrink count.
        /// </summary>
        /// <value>The shrink count.</value>
        public int ShrinkCount { get; private set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>The target.</value>
        public object? Target { get; }

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns>The copy.</returns>
        public RunResult Copy() => new(Method, Target, Parameters, ShrinkCount, MutationCount, ReturnedValue, Exception, ElapsedTime);

        /// <summary>
        /// Mutates the specified mutator.
        /// </summary>
        /// <param name="mutator">The mutator.</param>
        /// <param name="results">The results.</param>
        /// <param name="options">The options.</param>
        /// <returns>True if it is mutated, false otherwise.</returns>
        public bool Mutate(MutatorManager? mutator, List<RunResult> results, Options options)
        {
            options = options.Initialize();
            if (MutationCount >= options.MaxMutationCount || Exception is not null)
                return false;
            var Result = false;
            foreach (Parameter Parameter in Parameters)
            {
                Result |= Parameter.Mutate(mutator, results);
            }
            if (Result)
                ++MutationCount;
            return Result;
        }

        /// <summary>
        /// Runs the specified timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <param name="options">The options.</param>
        /// <returns>True if it runs successfully, false otherwise.</returns>
        public async Task<bool> RunAsync(Stopwatch timer, Options options)
        {
            if (Method.ContainsGenericParameters || timer is null)
                return false;
            bool Result;
            timer.Restart();
            try
            {
                ReturnedValue = Method.Invoke(Target, Parameters.ToArray(x => x?.Value));
                if (ReturnedValue is Task AwaitableReturnValue)
                    await AwaitableReturnValue.ConfigureAwait(false);
                Result = true;
            }
            catch (Exception E)
            {
                Result = options.ExceptionHandlers?.CanIgnore(E, Method) ?? false;
                if (!Result)
                {
                    Exception = E.InnerException ?? E;
                }
            }
            timer.Stop();
            ElapsedTime = timer.ElapsedMilliseconds;
            return Result;
        }

        /// <summary>
        /// Determines if this is the same as another run result.
        /// </summary>
        /// <param name="runResult">The run result.</param>
        /// <returns>True if they are the same, false otherwise.</returns>
        public bool Same(RunResult runResult)
        {
            if (runResult is null || runResult.Parameters.Length != Parameters.Length)
                return false;
            for (var X = 0; X < Parameters.Length; ++X)
            {
                if (!Parameters[X].Same(runResult.Parameters[X]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Shrinks the specified shrinker.
        /// </summary>
        /// <param name="shrinker">The shrinker.</param>
        /// <param name="results">The results.</param>
        /// <param name="options">The options.</param>
        /// <returns>True if it is shrunk, false otherwise.</returns>
        public bool Shrink(ShrinkerManager? shrinker, List<RunResult> results, Options options)
        {
            options = options.Initialize();
            if (ShrinkCount >= options.MaxShrinkCount || Exception is null)
                return false;
            var Result = false;
            foreach (Parameter Parameter in Parameters)
            {
                Result |= Parameter.Shrink(shrinker, results);
            }
            if (Result)
                ++ShrinkCount;
            return Result;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            if (Method is null)
                return "";
            var ReturnVal = "";
            var ShrinkText = "\n";
            var ExceptionText = "";
            if (Method.ReturnType != typeof(void))
                ReturnVal = $" => {ReturnedValue}";
            if (Exception is not null)
            {
                ShrinkText = $"\nNumber of shrinks:\t{ShrinkCount}\n";
                if (Parameters.Any(x => x.ShrinkCount > 1))
                    ShrinkText += Parameters.Where(x => x.ShrinkCount > 1).ToString(x => x.ToString(), "\n") + "\n";
                ExceptionText = $"\n\nException:\n{Exception}";
            }
            return $"{Method.Name} ({Parameters.ToString(x => GetValue(x), ", ")}){ReturnVal}{ShrinkText}Elapsed time:\t\t{ElapsedTime} ms{ExceptionText}\n\n-------------------------------------------------------------------------------------------------------------";
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string? GetValue(object? value)
        {
            if (value is null)
                return "null";
            if (value is string StringValue)
                return $"\"{StringValue}\"";
            if (value is char CharValue)
                return $"'{CharValue}'";
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

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The values of the parameter.</returns>
        private static string GetValue(Parameter value) => value.ParameterInfo.Name + ": " + GetValue(value.Value);
    }
}