using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mecha.Core
{
    /// <summary>
    /// Exception handler
    /// </summary>
    public class ExceptionHandler
    {
        /// <summary>
        /// Gets the exception handlers.
        /// </summary>
        /// <value>The exception handlers.</value>
        private Dictionary<Type, Func<Exception, MethodInfo, bool>> ExceptionHandlers { get; } = new Dictionary<Type, Func<Exception, MethodInfo, bool>>();

        /// <summary>
        /// Determines whether this instance can ignore the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="method">The method.</param>
        /// <returns>
        /// <c>true</c> if this instance can ignore the specified exception; otherwise, <c>false</c>.
        /// </returns>
        public bool CanIgnore(Exception exception, MethodInfo method)
        {
            return exception is null
                   || method is null
                   || (TryGetValue(exception.GetType(), out Func<Exception, MethodInfo, bool>? Handler)
                       ? Handler(exception, method)
                       : exception.InnerException is not null
                           && TryGetValue(exception.InnerException.GetType(), out Func<Exception, MethodInfo, bool>? InnerHandler)
                           && InnerHandler(exception.InnerException, method));
        }

        /// <summary>
        /// Adds an exception to the ignore list.
        /// </summary>
        /// <typeparam name="TException">The type of the exception.</typeparam>
        /// <param name="handler">The handler (if not provided, a default handler is used).</param>
        /// <returns>This.</returns>
        public ExceptionHandler IgnoreException<TException>(Func<Exception, MethodInfo, bool>? handler = null)
        {
            ExceptionHandlers.Add(typeof(TException), handler ?? DefaultHandler);
            return this;
        }

        /// <summary>
        /// Are the methods equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>True if they are, false otherwise.</returns>
        private static bool AreMethodsEqual(MethodBase? left, MethodBase? right)
        {
            if (left is null && right is null)
                return true;
            if (left is null || right is null)
                return false;
            if (left.Equals(right))
                return true;
            try
            {
                MethodInfo? RightMethod = left.DeclaringType?.GetMethod(right.Name, right.GetGenericArguments().Length, right.GetParameters().Select(p => p.ParameterType).ToArray());
                if (RightMethod is null)
                    return false;
                MethodInfo? LeftMethod = left.DeclaringType?.GetMethod(left.Name, left.GetGenericArguments().Length, left.GetParameters().Select(p => p.ParameterType).ToArray());
                return LeftMethod?.Equals(RightMethod) == true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Default handler for exceptions.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="method">The method.</param>
        /// <returns>True if it can be ignored, false otherwise.</returns>
        private static bool DefaultHandler(Exception exception, MethodInfo method)
        {
            MethodInfo GenericMethod = method.IsGenericMethod ? method.GetGenericMethodDefinition() : method;
            var ParameterCheckReturn = ParameterCheck(exception as ArgumentException, method.GetParameters());
            return typeof(Task).IsAssignableFrom(GenericMethod.ReturnType) || (AreMethodsEqual(GenericMethod, exception.TargetSite) && ParameterCheckReturn);
        }

        /// <summary>
        /// Parses the exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>True if it the parameter is found in the method, false otherwise.</returns>
        private static bool ParameterCheck(ArgumentException? exception, ParameterInfo[] parameters)
        {
            if (exception is null)
                return true;
            parameters ??= Array.Empty<ParameterInfo>();
            return parameters.Length == 0 || parameters.Any(x => x.Name == exception.ParamName || x.Name == exception.Message);
        }

        /// <summary>
        /// Tries to get the handler
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="handler">The handler.</param>
        /// <returns>True if it is found, false otherwise.</returns>
        private bool TryGetValue(Type type, out Func<Exception, MethodInfo, bool> handler)
        {
            Type? HandlerKey = ExceptionHandlers.Keys.FirstOrDefault(x => x.IsAssignableFrom(type));
            if (HandlerKey is null)
            {
                handler = (_, __) => false;
                return false;
            }
            handler = ExceptionHandlers[HandlerKey];
            return handler is not null;
        }
    }
}