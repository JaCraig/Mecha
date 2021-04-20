using Fast.Activator;
using Mecha.Core.Datasources;
using Mecha.Core.Exceptions;
using Mecha.Core.Generator;
using Mecha.Core.Runner;
using Mecha.Core.Shrinker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Mecha.Core
{
    /// <summary>
    /// Main class for breaking the
    /// </summary>
    public class Mech
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Check"/> class.
        /// </summary>
        /// <param name="generatorManager">The generator manager.</param>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="testRunnerManager">The test runner manager.</param>
        /// <param name="random">The random.</param>
        /// <param name="shrinker">The shrinker.</param>
        public Mech(GeneratorManager generatorManager, DataManager dataManager, TestRunnerManager testRunnerManager, Mirage.Random random, ShrinkerManager shrinker)
        {
            DataManager = dataManager;
            GeneratorManager = generatorManager;
            TestRunnerManager = testRunnerManager;
            Random = random;
            Shrinker = shrinker;
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static Mech? Default
        {
            get
            {
                if (Canister.Builder.Bootstrapper is null)
                {
                    lock (LockObject)
                    {
                        if (Canister.Builder.Bootstrapper is null)
                        {
                            new ServiceCollection().AddCanisterModules(configure => configure.RegisterMecha());
                        }
                    }
                }
                return Canister.Builder.Bootstrapper?.Resolve<Mech>();
            }
        }

        /// <summary>
        /// Gets the data manager.
        /// </summary>
        /// <value>The data manager.</value>
        public DataManager DataManager { get; }

        /// <summary>
        /// Gets the generator manager.
        /// </summary>
        /// <value>The generator manager.</value>
        public GeneratorManager GeneratorManager { get; }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Mirage.Random Random { get; }

        /// <summary>
        /// Gets the shrinker.
        /// </summary>
        /// <value>The shrinker.</value>
        public ShrinkerManager Shrinker { get; }

        /// <summary>
        /// Gets the test runner manager.
        /// </summary>
        /// <value>The test runner manager.</value>
        public TestRunnerManager TestRunnerManager { get; }

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// Breaks the specified target.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="AggregateException"></exception>
        public static async Task BreakAsync<TClass>(TClass target, Options? options = null)
        {
            if (target is null || Default is null)
                return;
            options ??= Options.Default;
            var ClassType = target.GetType();
            var Exceptions = new List<Exception>();
            foreach (var Method in ClassType.GetMethods())
            {
                if (!(Method.GetCustomAttribute<DoNotBreakAttribute>() is null))
                    continue;
                var Result = await Default.RunAsync(Method, target, options).ConfigureAwait(false);
                if (!(Result.Exception is null))
                    Exceptions.Add(Result.Exception);
            }
            if (Exceptions.Count > 0)
                throw new AggregateException(Exceptions);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <typeparam name="TValue1">The type of the value1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1>(Action<TValue1> action, Options? options = null)
        {
            return BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <typeparam name="TValue1">The type of the value1.</typeparam>
        /// <typeparam name="TValue2">The type of the value2.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1, TValue2>(Action<TValue1, TValue2> action, Options? options = null)
        {
            return BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <typeparam name="TValue1">The type of the value1.</typeparam>
        /// <typeparam name="TValue2">The type of the value2.</typeparam>
        /// <typeparam name="TValue3">The type of the value3.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1, TValue2, TValue3>(Action<TValue1, TValue2, TValue3> action, Options? options = null)
        {
            return BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <typeparam name="TValue1">The type of the value1.</typeparam>
        /// <typeparam name="TValue2">The type of the value2.</typeparam>
        /// <typeparam name="TValue3">The type of the value3.</typeparam>
        /// <typeparam name="TValue4">The type of the value4.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1, TValue2, TValue3, TValue4>(Action<TValue1, TValue2, TValue3, TValue4> action, Options? options = null)
        {
            return BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <typeparam name="TValue1">The type of the value1.</typeparam>
        /// <typeparam name="TValue2">The type of the value2.</typeparam>
        /// <typeparam name="TValue3">The type of the value3.</typeparam>
        /// <typeparam name="TValue4">The type of the value4.</typeparam>
        /// <typeparam name="TValue5">The type of the value5.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1, TValue2, TValue3, TValue4, TValue5>(Action<TValue1, TValue2, TValue3, TValue4, TValue5> action, Options? options = null)
        {
            return BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <typeparam name="TValue1">The type of the value1.</typeparam>
        /// <typeparam name="TValue2">The type of the value2.</typeparam>
        /// <typeparam name="TValue3">The type of the value3.</typeparam>
        /// <typeparam name="TValue4">The type of the value4.</typeparam>
        /// <typeparam name="TValue5">The type of the value5.</typeparam>
        /// <typeparam name="TValue6">The type of the value6.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6>(Action<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6> action, Options? options = null)
        {
            return BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <typeparam name="TValue1">The type of the value1.</typeparam>
        /// <typeparam name="TValue2">The type of the value2.</typeparam>
        /// <typeparam name="TValue3">The type of the value3.</typeparam>
        /// <typeparam name="TValue4">The type of the value4.</typeparam>
        /// <typeparam name="TValue5">The type of the value5.</typeparam>
        /// <typeparam name="TValue6">The type of the value6.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7>(Action<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7> action, Options? options = null)
        {
            return BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync(Action action, Options? options = null)
        {
            return BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        public static async Task BreakAsync(MethodInfo method, object? target, Options? options = null)
        {
            if (Default is null || !(method.GetCustomAttribute<DoNotBreakAttribute>() is null))
                return;
            options ??= Options.Default;
            var Result = await Default.RunAsync(method, target, options).ConfigureAwait(false);
            if (!Result.Passed)
                throw new MethodBrokenException(Result.Output, Result.Exception);
        }

        /// <summary>
        /// Finds the methods of the class type specified and tries to break them.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <returns>The async task.</returns>
        public static Task BreakAsync<TClass>(Options? options = null)
        {
            var ClassType = typeof(TClass);
            if (ClassType.IsAbstract && ClassType.IsSealed) //Static class
                return Task.CompletedTask;
            return BreakAsync(FastActivator.CreateInstance(ClassType), options);
        }

        /// <summary>
        /// Runs the specified method.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>The result</returns>
        public Task<Result> RunAsync(MethodInfo runMethod, object? target, Options? options)
        {
            return TestRunnerManager.RunAsync(runMethod, target, options ?? Options.Default);
        }
    }
}