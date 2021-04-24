using BigBook;
using Mecha.Core.Generator;
using Mecha.Core.Runner.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;

namespace Mecha.Core.Runner.BaseClasses
{
    /// <summary>
    /// Runner base class
    /// </summary>
    public abstract class RunnerBaseClass : IRunner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunnerBaseClass"/> class.
        /// </summary>
        protected RunnerBaseClass(Mirage.Random random)
        {
            Random = random;
        }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        protected Mirage.Random Random { get; }

        /// <summary>
        /// Gets or sets the manager.
        /// </summary>
        /// <value>The manager.</value>
        protected Mech? Manager { get; set; }

        /// <summary>
        /// Runs the specified method on the target class.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>The result.</returns>
        public async Task<Result> RunAsync(MethodInfo runMethod, object? target, Options options)
        {
            if (runMethod is null)
                return new Result { Output = "Method not specified", Passed = false, Exception = new ArgumentNullException(nameof(runMethod)) };
            options ??= Options.Default;
            if (options.MaxDuration == 0)
                options.MaxDuration = 1;
            Init();
            StartRun(runMethod, target, options);
            var Count = options.GenerationCount;
            var TempTimer = new Stopwatch();
            bool Finished = false;

            var Results = ReloadTests(runMethod).ConvertAll(x => new RunResult(runMethod, target, x));
            var GeneratedParameters = GenerateArguments(runMethod, options);
            using (var InternalTimer = new Timer(options.MaxDuration))
            {
                InternalTimer.Elapsed += (sender, e) => Finished = true;
                InternalTimer.Start();
                for (var x = Results.Count; x < Count; ++x)
                {
                    if (Finished)
                        break;
                    var Arguments = new object?[GeneratedParameters.Length];
                    for (var y = 0; y < GeneratedParameters.Length; ++y)
                    {
                        Arguments[y] = Random.Next(GeneratedParameters[y].GeneratedValues);
                    }
                    if (!Results.AddIfUnique((val1, val2) => val1.Same(val2), new RunResult(runMethod, target, Arguments)))
                        --x;
                }
                InternalTimer.Stop();
            }

            Finished = false;
            using (var InternalTimer = new Timer(options.MaxDuration))
            {
                InternalTimer.Elapsed += (sender, e) => Finished = true;
                InternalTimer.Start();
                var Tasks = new List<Task>();
                for (var x = 0; x < Results.Count; ++x)
                {
                    if (Finished)
                        break;
                    Tasks.Add(Results[x].RunAsync(TempTimer));
                }
                await Task.WhenAll(Tasks).ConfigureAwait(false);
                InternalTimer.Stop();
            }

            Results = await ShrinkAsync(Results, options).ConfigureAwait(false);
            Manager?.DataManager.Clear(runMethod);
            foreach (var Result in Results.Where(x => !(x.Exception is null)))
            {
                SaveArguments(runMethod, Result.Parameters.ToArray(x => x?.Value));
            }
            return FinishRun(runMethod, target, options, Results);
        }

        /// <summary>
        /// Finishes the run and converts the list of runs to a finished result.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <param name="results">The results.</param>
        /// <returns>The result for the run.</returns>
        protected abstract Result FinishRun(MethodInfo runMethod, object? target, Options options, List<RunResult> results);

        /// <summary>
        /// Generates the arguments.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <param name="options">The options.</param>
        /// <returns>The generated arguments.</returns>
        protected ParameterValues[] GenerateArguments(MethodInfo methodInfo, Options options)
        {
            return Manager?.GeneratorManager.GenerateParameterValues(methodInfo.GetParameters(), options) ?? Array.Empty<ParameterValues>();
        }

        /// <summary>
        /// Reloads the arguments.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns></returns>
        protected List<object?[]> ReloadTests(MethodInfo methodInfo)
        {
            return Manager?.DataManager.Read(methodInfo) ?? new List<object?[]>();
        }

        /// <summary>
        /// Saves the arguments.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="data">The data.</param>
        protected void SaveArguments(MethodInfo method, object?[] data)
        {
            if (data is null || data.Length == 0)
                return;
            Manager?.DataManager.Save(method, data);
        }

        /// <summary>
        /// Shrinks this instance.
        /// </summary>
        /// <param name="runs">The runs.</param>
        /// <param name="options">The options.</param>
        /// <returns>The shrunk run.</returns>
        protected async Task<List<RunResult>> ShrinkAsync(List<RunResult> runs, Options options)
        {
            var FinalRuns = runs.Where(x => x.Exception is null).ToList();
            var FailedRuns = runs.Where(x => !(x.Exception is null)).ToList();
            var TempTimer = new Stopwatch();
            for (var x = 0; x < FailedRuns.Count; ++x)
            {
                var CurrentRun = FailedRuns[x];
                if (CurrentRun is null)
                    continue;
                var CopiedRun = CurrentRun.Copy();
                while (CurrentRun.Shrink(Manager?.Shrinker, FinalRuns, options))
                {
                    if (await CurrentRun.RunAsync(TempTimer).ConfigureAwait(false))
                    {
                        break;
                    }
                    CopiedRun = CurrentRun.Copy();
                }
                FinalRuns.Add(CopiedRun);
            }
            for (var x = 0; x < FinalRuns.Count; ++x)
            {
                var Runs = FinalRuns.FindAll(y => FinalRuns[x].Same(y));
                for (var y = 1; y < Runs.Count; ++y)
                {
                    FinalRuns.Remove(Runs[y]);
                }
            }
            return FinalRuns;
        }

        /// <summary>
        /// Called at the start of the run.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        protected abstract void StartRun(MethodInfo runMethod, object? target, Options options);

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        private void Init()
        {
            Manager = Mech.Default;
        }
    }
}