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
        /// Gets or sets the manager.
        /// </summary>
        /// <value>The manager.</value>
        protected Mech? Manager { get; set; }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        protected Mirage.Random Random { get; }

        /// <summary>
        /// Runs the specified method on the target class.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>The result.</returns>
        public async Task<Result> RunAsync(MethodInfo? runMethod, object? target, Options options)
        {
            if (runMethod is null)
                return new Result { Output = "Method not specified", Passed = false, Exception = new ArgumentNullException(nameof(runMethod)) };
            options = options.Initialize();
            Init();
            StartRun(runMethod, target, options);
            var Count = options.GenerationCount;
            var TempTimer = new Stopwatch();
            var Finished = false;

            List<RunResult> Results = ReloadTests(runMethod).ConvertAll(x => new RunResult(runMethod, target, x));
            ParameterValues[] GeneratedParameters = GenerateArguments(runMethod, options);
            using (var InternalTimer = new Timer(options.MaxDuration))
            {
                InternalTimer.Elapsed += (sender, e) => Finished = true;
                InternalTimer.Start();
                for (var x = Results.Count; x < Count; ++x)
                {
                    if (Finished)
                        break;
                    var Arguments = new ParameterValue?[GeneratedParameters.Length];
                    for (var y = 0; y < GeneratedParameters.Length; ++y)
                    {
                        Arguments[y] = Random.Next(GeneratedParameters[y].GeneratedValues);
                    }
                    if (!Results.AddIfUnique(IsTheSame, new RunResult(runMethod, target, Arguments)))
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
                    Tasks.Add(Results[x].RunAsync(TempTimer, options));
                }
                await Task.WhenAll(Tasks).ConfigureAwait(false);
                InternalTimer.Stop();
            }
            Results = await MutateAsync(Results, options).ConfigureAwait(false);
            Results = await ShrinkAsync(Results, options).ConfigureAwait(false);
            Manager?.DataManager.Clear(runMethod);
            foreach (RunResult? Result in Results.Where(x => x.Exception is not null))
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
        protected ParameterValues[] GenerateArguments(MethodInfo methodInfo, Options options) => Manager?.GeneratorManager.GenerateParameterValues(methodInfo.GetParameters(), options) ?? Array.Empty<ParameterValues>();

        /// <summary>
        /// Attempts to mutate the successful runs asynchronously.
        /// </summary>
        /// <param name="runs">The runs.</param>
        /// <param name="options">The options.</param>
        /// <returns>The resulting runs.</returns>
        protected async Task<List<RunResult>> MutateAsync(List<RunResult> runs, Options options)
        {
            var SuccessfulRuns = runs.Where(x => x.Exception is null).ToList();
            var TempTimer = new Stopwatch();
            for (var x = 0; x < SuccessfulRuns.Count; ++x)
            {
                RunResult? CurrentRun = SuccessfulRuns[x];
                if (CurrentRun is null)
                    continue;
                RunResult CopiedRun = CurrentRun.Copy();
                while (CopiedRun.Mutate(Manager?.Mutator, runs, options))
                {
                    if (!await CopiedRun.RunAsync(TempTimer, options).ConfigureAwait(false))
                    {
                        break;
                    }
                    CopiedRun = CopiedRun.Copy();
                }
                if (CopiedRun.Exception is not null)
                    runs.Add(CopiedRun);
            }
            ShrinkRunsReported(runs);
            return runs;
        }

        /// <summary>
        /// Reloads the arguments.
        /// </summary>
        /// <param name="methodInfo">The method information.</param>
        /// <returns></returns>
        protected List<object?[]> ReloadTests(MethodInfo methodInfo) => Manager?.DataManager.Read(methodInfo) ?? new List<object?[]>();

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
            var FailedRuns = runs.Where(x => x.Exception is not null).ToList();
            var TempTimer = new Stopwatch();
            for (var x = 0; x < FailedRuns.Count; ++x)
            {
                RunResult? CurrentRun = FailedRuns[x];
                if (CurrentRun is null)
                    continue;
                RunResult CopiedRun = CurrentRun.Copy();
                while (CurrentRun.Shrink(Manager?.Shrinker, FinalRuns, options))
                {
                    if (await CurrentRun.RunAsync(TempTimer, options).ConfigureAwait(false))
                    {
                        break;
                    }
                    CopiedRun = CurrentRun.Copy();
                }
                FinalRuns.Add(CopiedRun);
            }
            ShrinkRunsReported(FinalRuns);
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
        /// Determines whether [is the same] [the specified val1].
        /// </summary>
        /// <param name="val1">The val1.</param>
        /// <param name="val2">The val2.</param>
        /// <returns><c>true</c> if [is the same] [the specified val1]; otherwise, <c>false</c>.</returns>
        private static bool IsTheSame(RunResult val1, RunResult val2) => val1.Same(val2);

        /// <summary>
        /// Shrinks the runs reported.
        /// </summary>
        /// <param name="FinalRuns">The final runs.</param>
        private static void ShrinkRunsReported(List<RunResult> FinalRuns)
        {
            for (var x = 0; x < FinalRuns.Count; ++x)
            {
                List<RunResult> Runs = FinalRuns.FindAll(y => FinalRuns[x].Same(y));
                for (var y = 1; y < Runs.Count; ++y)
                {
                    _ = FinalRuns.Remove(Runs[y]);
                }
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        private void Init() => Manager = Mech.Default;
    }
}