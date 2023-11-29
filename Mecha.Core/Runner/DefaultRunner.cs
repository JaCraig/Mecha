using BigBook;
using Mecha.Core.Runner.BaseClasses;
using Mecha.Core.Runner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mecha.Core.Runner
{
    /// <summary>
    /// Default runner
    /// </summary>
    /// <seealso cref="IRunner"/>
    public class DefaultRunner : RunnerBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRunner"/> class.
        /// </summary>
        /// <param name="random"></param>
        public DefaultRunner(Mirage.Random random) : base(random)
        {
        }

        /// <summary>
        /// Finishes the run and converts the list of runs to a finished result.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <param name="results">The results.</param>
        /// <returns>The result for the run.</returns>
        protected override Result FinishRun(MethodInfo runMethod, object? target, Options options, List<RunResult> results)
        {
            var ReturnValue = new Result
            {
                ExecutionTime = results.Sum(x => x.ElapsedTime)
            };
            if (results.Any(x => x.Exception is not null))
            {
                Exception?[] Exceptions = results.Where(x => x.Exception is not null).Select(x => x.Exception).ToArray();
                ReturnValue.Passed = false;
                ReturnValue.Exception = Exceptions.Length == 1 ? Exceptions[0] : new AggregateException("Run failed with the following exceptions", Exceptions!);
                ReturnValue.Output = $"The run failed with the following stats:\n\n{results.Count} generations.\n{Exceptions.Length} exceptions.\n\nFailed Tests:\n\n{results.Where(x => x.Exception is not null).ToString(x => x.ToString(), "\n\n")}\n\nPassed Tests:\n\n{results.Where(x => x.Exception is null).ToString(x => x.ToString(), "\n\n")}";
            }
            else
            {
                ReturnValue.Passed = true;
                ReturnValue.Output = $"The run passed with the following stats:\n\n{results.Count} generations\n\nTests:\n\n{results.ToString(x => x.ToString(), "\n\n")}";
            }
            return ReturnValue;
        }

        /// <summary>
        /// Called at the start of the run.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        protected override void StartRun(MethodInfo runMethod, object? target, Options options)
        {
        }
    }
}