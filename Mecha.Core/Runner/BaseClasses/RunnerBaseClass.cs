using BigBook;
using Mecha.Core.Generator;
using Mecha.Core.Runner.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

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
        protected Check? Manager { get; set; }

        /// <summary>
        /// Runs the specified method on the target class.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>The result.</returns>
        public Task<Result> RunAsync(MethodInfo runMethod, object? target, Options options)
        {
            Init();
            StartRun(runMethod, target, options);
            var Results = new List<RunResult>();
            var Parameters = runMethod.GetParameters();
            var Count = options.GenerationCount;
            var TempTimer = new Stopwatch();
            var PreviousData = ReloadArguments(runMethod);
            decimal TotalTime = 0;
            for (var x = 0; x < PreviousData.Count; ++x)
            {
                if (TotalTime >= options.MaxDuration)
                    break;
                if (PreviousData[x].Length != Parameters.Length)
                    continue;
                TempTimer.Restart();
                var CurrentRun = GenerateRun(runMethod, Parameters, target, PreviousData[x]);
                TempTimer.Stop();
                TotalTime += CurrentRun.ElapsedTime = TempTimer.ElapsedMilliseconds;
                Results.Add(CurrentRun);
            }
            var GeneratedParameters = GenerateArguments(runMethod, options);
            for (var x = PreviousData.Count; x < Count; ++x)
            {
                if (TotalTime >= options.MaxDuration)
                    break;
                TempTimer.Restart();
                var Arguments = new object?[Parameters.Length];
                for (var y = 0; y < Parameters.Length; ++y)
                {
                    Arguments[y] = Random.Next(GeneratedParameters[y].GeneratedValues);
                }
                if (PreviousData.AddIfUnique(Same, Arguments))
                {
                    var CurrentRun = GenerateRun(runMethod, Parameters, target, Arguments);
                    TempTimer.Stop();
                    TotalTime += CurrentRun.ElapsedTime = TempTimer.ElapsedMilliseconds;
                    Results.Add(CurrentRun);
                }
                else
                {
                    TempTimer.Stop();
                    TotalTime += TempTimer.ElapsedMilliseconds;
                }
            }
            Results = Shrink(Results, options);
            Manager?.DataManager.Clear(runMethod);
            foreach (var Result in Results.Where(x => !(x.Exception is null)))
            {
                SaveArguments(runMethod, Result.ParametersUsed);
            }
            return Task.FromResult(FinishRun(runMethod, target, options, Results));
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
        protected List<object?[]> ReloadArguments(MethodInfo methodInfo)
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
        protected List<RunResult> Shrink(List<RunResult> runs, Options options)
        {
            var FinalRuns = runs.Where(x => x.Exception is null).ToList();
            var FailedRuns = runs.Where(x => !(x.Exception is null)).ToList();
            for (var x = 0; x < FailedRuns.Count; ++x)
            {
                var CurrentRun = FailedRuns[x];
                if (CurrentRun is null)
                    continue;
                var Parameters = new object?[CurrentRun.ParametersUsed.Length];
                for (var y = 0; y < CurrentRun.ParametersUsed.Length; ++y)
                {
                    Parameters[y] = CurrentRun.ParametersUsed[y];
                    if (!FinalRuns.Any(x => Same(x.ParametersUsed[y], CurrentRun.ParametersUsed[y]) && x.Exception is null))
                        Parameters[y] = Manager?.Shrinker.Shrink(CurrentRun.ParametersUsed[y]) ?? CurrentRun.ParametersUsed[y];
                }
                if (Same(Parameters, CurrentRun.ParametersUsed))
                {
                    FinalRuns.Add(CurrentRun);
                    continue;
                }
                var ShrunkResult = GenerateRun(CurrentRun.Method, CurrentRun.Parameters, CurrentRun.Target, Parameters);
                if (ShrunkResult.Exception is null)
                {
                    FinalRuns.Add(CurrentRun);
                    continue;
                }
                ShrunkResult.ShrinkCount = CurrentRun.ShrinkCount + 1;
                if (ShrunkResult.ShrinkCount >= options.MaxShrinkCount)
                {
                    FinalRuns.Add(ShrunkResult);
                    continue;
                }
                FailedRuns.Add(ShrunkResult);
            }
            for (var x = 0; x < FinalRuns.Count; ++x)
            {
                var Runs = FinalRuns.FindAll(y => Same(y.ParametersUsed, FinalRuns[x].ParametersUsed));
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
        /// Generates the run.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>The run's result.</returns>
        private static RunResult GenerateRun(MethodInfo runMethod, ParameterInfo[] parameters, object? target, object?[] arguments)
        {
            var CurrentRun = new RunResult
            {
                Method = runMethod,
                Parameters = parameters,
                Target = target,
                ParametersUsed = arguments
            };
            try
            {
                CurrentRun.ReturnedValue = CurrentRun.Method.Invoke(CurrentRun.Target, CurrentRun.ParametersUsed);
            }
            catch (Exception e)
            {
                CurrentRun.Exception = e;
            }
            return CurrentRun;
        }

        /// <summary>
        /// Determines if the 2 arrays are the same.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>True if they are, false otherwise.</returns>
        private static bool Same(object?[] value1, object?[] value2)
        {
            if (value1 is null || value2 is null)
                return false;
            if (value1.Length != value2.Length)
                return false;
            for (int x = 0; x < value1.Length; ++x)
            {
                var Value1 = JsonSerializer.Serialize(value1[x]);
                var Value2 = JsonSerializer.Serialize(value2[x]);
                if (Value1 != Value2)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Sames the specified value1.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>True if they are the same, false otherwise.</returns>
        private static bool Same(object? value1, object? value2)
        {
            if (value1 is null && value2 is null)
                return true;
            if (value1 is null || value2 is null)
                return false;
            var Value1 = JsonSerializer.Serialize(value1);
            var Value2 = JsonSerializer.Serialize(value2);
            return Value1 == Value2;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        private void Init()
        {
            Manager = Check.Default;
        }
    }
}