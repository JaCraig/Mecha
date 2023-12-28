using BigBook;
using Fast.Activator;
using FileCurator;
using Mecha.Core.Datasources;
using Mecha.Core.Exceptions;
using Mecha.Core.ExtensionMethods;
using Mecha.Core.Generator;
using Mecha.Core.Generator.DefaultGenerators.Utils;
using Mecha.Core.Mutator;
using Mecha.Core.Runner;
using Mecha.Core.Shrinker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
        /// Initializes a new instance of the <see cref="Mech"/> class.
        /// </summary>
        /// <param name="generatorManager">The generator manager.</param>
        /// <param name="dataManager">The data manager.</param>
        /// <param name="testRunnerManager">The test runner manager.</param>
        /// <param name="random">The random.</param>
        /// <param name="shrinker">The shrinker.</param>
        /// <param name="mutator">The mutator.</param>
        public Mech(GeneratorManager generatorManager, DataManager dataManager, TestRunnerManager testRunnerManager, Mirage.Random random, ShrinkerManager shrinker, MutatorManager mutator)
        {
            DataManager = dataManager;
            GeneratorManager = generatorManager;
            TestRunnerManager = testRunnerManager;
            Random = random;
            Shrinker = shrinker;
            Mutator = mutator;
            try
            {
                _ = new DirectoryInfo("./Mech").EnumerateFiles().ForEach(x => x.Delete());
            }
            catch { }
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static Mech? Default
        {
            get
            {
                if (_Default is not null)
                    return _Default;
                lock (_LockObject)
                {
                    if (_Default is not null)
                        return _Default;
                    new System.IO.DirectoryInfo("./Mecha").Create();
                    _Default = Services.ServiceProvider.GetService<Mech>();
                }
                return _Default;
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
        /// Gets the mutator.
        /// </summary>
        /// <value>The mutator.</value>
        public MutatorManager? Mutator { get; }

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
        private static readonly object _LockObject = new();

        /// <summary>
        /// The default
        /// </summary>
        private static Mech? _Default;

        /// <summary>
        /// Breaks the specified target.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="AggregateException"></exception>
        public static Task BreakAsync<TClass>(TClass target, Options? options = null) => target is null || Default is null ? Task.CompletedTask : BreakAsync(target, target.GetType(), options);

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <typeparam name="TValue1">The type of the value1.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1>(Action<TValue1> action, Options? options = null)
        {
            return action is null
                ? Task.CompletedTask
                : BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
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
            return action is null
                ? Task.CompletedTask
                : BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
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
            return action is null
                ? Task.CompletedTask
                : BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
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
            return action is null
                ? Task.CompletedTask
                : BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
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
            return action is null
                ? Task.CompletedTask
                : BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
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
            return action is null
                ? Task.CompletedTask
                : BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
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
        /// <typeparam name="TValue7">The type of the value7.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7>(Action<TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7> action, Options? options = null)
        {
            return action is null
                ? Task.CompletedTask
                : BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public static Task BreakAsync(Action action, Options? options = null)
        {
            return action is null
                ? Task.CompletedTask
                : BreakAsync(action.Method, FastActivator.CreateInstance(action.Method.DeclaringType), options);
        }

        /// <summary>
        /// Breaks the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        public static async Task BreakAsync(MethodInfo method, object? target, Options? options = null)
        {
            if (Default is null)
                return;
            Result Result = await Default.RunAsync(method, target, options).ConfigureAwait(false);
            if (!Result.Passed)
                throw new MethodBrokenException(Result.Output, Result.Exception);
        }

        /// <summary>
        /// Finds the methods of the class type specified and tries to break them.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <returns>The async task.</returns>
        public static Task BreakAsync<TClass>(Options? options = null) => BreakAsync(typeof(TClass), options);

        /// <summary>
        /// Finds the methods of the class type specified and tries to break them.
        /// </summary>
        /// <param name="classType">Type of the class.</param>
        /// <param name="options">The options.</param>
        /// <returns>The async task.</returns>
        public static Task BreakAsync(Type classType, Options? options = null)
        {
            if (classType is null)
            {
                return Task.CompletedTask;
            }
            else
            {
                return classType.IsAbstract && classType.IsSealed
                    ? BreakAsync(default(object), classType, options)
                    : BreakAsync(FastActivator.CreateInstance(classType), options);
            }
        }

        /// <summary>
        /// Runs the specified method.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>The result</returns>
        public Task<Result> RunAsync(MethodInfo? runMethod, object? target, Options? options)
        {
            if (SkipMethod(runMethod, target, options))
            {
                return Task.FromResult(Result.Skipped);
            }
            runMethod = FixMethod(runMethod);
            return runMethod?.IsGenericMethodDefinition != false
                ? Task.FromResult(Result.Skipped)
                : TestRunnerManager.RunAsync(runMethod, target, options.Initialize());
        }

        /// <summary>
        /// Attempts to find the type using basic lookup.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The type.</returns>
        private static Type? AttemptToFindBasic(Type args) => Array.Find(BasicTypesLookup.Types, y => args.BaseType?.IsAssignableFrom(y) == true && args.GetInterfaces().All(z => z.IsAssignableFrom(y)));

        /// <summary>
        /// Attempts to resolve the type.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="type">The type.</param>
        /// <returns>The type</returns>
        private static Type? AttemptToResolveType(Type arg, Type? type)
        {
            if (type is not null)
                return type;
            try
            {
                if (arg.BaseType is not null)
                    return Services.ServiceProvider.GetService(arg.BaseType)?.GetType() ?? type;
            }
            catch
            {
            }
            foreach (Type Interface in arg.GetInterfaces())
            {
                try
                {
                    return Services.ServiceProvider.GetService(Interface)?.GetType() ?? type;
                }
                catch
                {
                }
            }

            return type;
        }

        /// <summary>
        /// Attempts to create via substitute.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="type">The type.</param>
        /// <returns>The types</returns>
        private static Type? AttemptToSubstitute(Type args, Type? type)
        {
            if (type is not null)
                return type;
            List<Type> FinalTypes = args.BaseType is not null ? new List<Type> { args.BaseType } : new List<Type>();
            FinalTypes.AddRange(args.GetInterfaces());
            try
            {
                return NSubstitute.Substitute.For(FinalTypes.ToArray(), Array.Empty<object>())?.GetType() ?? type;
            }
            catch { }
            return type;
        }

        /// <summary>
        /// Breaks the asynchronous.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="AggregateException"></exception>
        private static async Task BreakAsync(object? target, Type classType, Options? options)
        {
            if (Default is null)
                return;
            var Exceptions = new List<Exception>();
            foreach (MethodInfo Method in classType.GetMethods())
            {
                Result Result = await Default.RunAsync(Method, target, options).ConfigureAwait(false);
                if (Result.Exception is not null)
                    Exceptions.Add(Result.Exception);
            }
            if (Exceptions.Count > 0)
                throw new AggregateException(Exceptions);
        }

        /// <summary>
        /// Fixes the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The fixed method</returns>
        private static MethodInfo? FixMethod(MethodInfo? method)
        {
            if (method is null)
                return method;
            if (!method.IsGenericMethodDefinition)
                return method;
            Type[] Args = method.GetGenericArguments();
            var ResultingItems = new Type[Args.Length];
            for (var X = 0; X < ResultingItems.Length; ++X)
            {
                Type? Type = AttemptToFindBasic(Args[X]);
                Type = AttemptToSubstitute(Args[X], Type);
                Type = AttemptToResolveType(Args[X], Type);
                if (Type is null)
                    return method;
                ResultingItems[X] = Type;
            }
            return method.MakeGenericMethod(ResultingItems);
        }

        /// <summary>
        /// Skips the method.
        /// </summary>
        /// <param name="runMethod">The run method.</param>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <returns>True if it should be skipped, false otherwise.</returns>
        private static bool SkipMethod(MethodInfo? runMethod, object? target, Options? options)
        {
            return runMethod is null
                            || runMethod.GetCustomAttribute<DoNotBreakAttribute>() is not null
                            || (target is null && runMethod.DeclaringType == typeof(object))
                            || (runMethod.DeclaringType == typeof(MarshalByRefObject))
                            || (runMethod.DeclaringType == typeof(DynamicObject))
                            || (!(options?.DiscoverInheritedMethods ?? true) && runMethod.DeclaringType != target?.GetType());
        }
    }
}