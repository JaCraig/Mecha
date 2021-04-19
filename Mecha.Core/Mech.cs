using Fast.Activator;
using Mecha.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Mecha.Core
{
    /// <summary>
    /// Main class for breaking the
    /// </summary>
    public static class Mech
    {
        /// <summary>
        /// Breaks the specified target.
        /// </summary>
        /// <typeparam name="TClass">The type of the class.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="AggregateException"></exception>
        public static async Task BreakAsync<TClass>(TClass target, Options? options = null)
        {
            if (target is null || Check.Default is null)
                return;
            options ??= Options.Default;
            var ClassType = target.GetType();
            var Exceptions = new List<Exception>();
            foreach (var Method in ClassType.GetMethods())
            {
                var Result = await Check.Default.RunAsync(Method, target, options).ConfigureAwait(false);
                if (!Result.Passed)
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
            if (Check.Default is null)
                return;
            options ??= Options.Default;
            var Result = await Check.Default.RunAsync(method, target, options).ConfigureAwait(false);
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
    }
}