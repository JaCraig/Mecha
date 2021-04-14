using Mecha.Core.Runner.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

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
        protected RunnerBaseClass()
        {
        }

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
        public Result Run(MethodInfo runMethod, object? target, Options options)
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
            for (var x = PreviousData.Count; x < Count; ++x)
            {
                if (TotalTime >= options.MaxDuration)
                    break;
                TempTimer.Restart();
                var Arguments = GenerateArguments(runMethod, options, PreviousData);
                if (Arguments.Length != Parameters.Length)
                    break;
                PreviousData.Add(Arguments);
                var CurrentRun = GenerateRun(runMethod, Parameters, target, Arguments);
                TempTimer.Stop();
                TotalTime += CurrentRun.ElapsedTime = TempTimer.ElapsedMilliseconds;
                Results.Add(CurrentRun);
            }
            Results = Shrink(Results, options);
            Manager?.DataManager.Clear(runMethod);
            foreach (var Result in Results.Where(x => !(x.Exception is null)))
            {
                SaveArguments(runMethod, Result.ParametersUsed);
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
        /// <param name="previousData">The previous data.</param>
        /// <returns>The generated arguments.</returns>
        protected object?[] GenerateArguments(MethodInfo methodInfo, Options options, List<object?[]> previousData)
        {
            return Manager?.GeneratorManager.GenerateData(methodInfo, options, previousData) ?? Array.Empty<object>();
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
            return runs;
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
        /// Initializes this instance.
        /// </summary>
        /// <returns></returns>
        private void Init()
        {
            Manager = Check.Default;
        }
    }
}