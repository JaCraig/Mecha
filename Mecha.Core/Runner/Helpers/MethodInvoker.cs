﻿using Mecha.Core.ExtensionMethods;
using Mecha.Core.Runner.Interfaces;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Mecha.Core.Runner.Helpers
{
    /// <summary>
    /// Method invoker
    /// </summary>
    /// <remarks>Method invoker constructor</remarks>
    /// <remarks>Initializes a new instance of the <see cref="MethodInvoker{TTarget}"/> class.</remarks>
    public class MethodInvoker<TTarget> : IMethodInvoker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodInvoker{TTarget}"/> class.
        /// </summary>
        /// <param name="method">The method.</param>
        public MethodInvoker(MethodInfo method)
        {
            Method = method;
            InternalExpression = MethodInvoker<TTarget>.CreateExpression(method) ?? ((__, _) => throw new Exception("Couldn't create invoke method and InternalExpression is null"));
        }

        /// <summary>
        /// The method to invoke
        /// </summary>
        public MethodInfo? Method { get; set; }

        /// <summary>
        /// The internal expression
        /// </summary>
        private Func<TTarget?, object?[], object?> InternalExpression { get; }

        /// <summary>
        /// Invokes the method
        /// </summary>
        /// <param name="target">The target</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>The result</returns>
        public object? Invoke(object? target, object?[] parameters)
        {
            try
            {
                return InternalExpression((TTarget?)target, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create the expression
        /// </summary>
        /// <param name="method">The method</param>
        private static Func<TTarget?, object?[], object?> CreateExpression(MethodInfo method)
        {
            ParameterExpression Target = Expression.Parameter(typeof(TTarget?), "target");
            ParameterInfo[] Parameters = method.GetParameters();
            ParameterExpression Params = Expression.Parameter(typeof(object?[]), "params");
            var ParamsExpressions = new Expression[Parameters.Length];
            for (var X = 0; X < Parameters.Length; ++X)
            {
                ParameterInfo Parameter = Parameters[X];
                Type? ParameterType = Parameter.ParameterType;
                if (ParameterType.IsByRef)
                {
                    ParameterType = ParameterType.GetElementType();
                }
                ParamsExpressions[X] = Expression.ArrayIndex(Params, Expression.Constant(X));
                if (Parameter.ParameterType.IsSpecialType(out Type? SpecialType) && SpecialType is not null)
                {
                    Type? ParameterElementType = ParameterType.GetGenericArguments()[0];
                    Type? ParameterArrayType = ParameterElementType.MakeArrayType();
                    ParamsExpressions[X] = Expression.Convert(ParamsExpressions[X], ParameterArrayType);
                    Type? GenericParameterElementType = SpecialType.MakeGenericType(ParameterElementType!);
                    ConstructorInfo? SpecialConstructor = GenericParameterElementType.GetConstructor(new[] { ParameterArrayType });
                    ParamsExpressions[X] = Expression.New(SpecialConstructor!, ParamsExpressions[X]);
                }
                else
                {
                    ParamsExpressions[X] = Expression.Convert(ParamsExpressions[X], ParameterType!);
                }
            }
            Expression MethodCall = method.IsStatic ? Expression.Call(null, method, ParamsExpressions) : (Expression)Expression.Call(Target, method, ParamsExpressions);
            if (method.ReturnType == typeof(void))
            {
                return CreateVoidMethod(Target, Params, MethodCall);
            }
            else if (method.ReturnType != typeof(object))
            {
                MethodCall = Expression.Convert(MethodCall, typeof(object));
            }
            var Lambda = Expression.Lambda<Func<TTarget?, object?[], object?>>(MethodCall, Target, Params);
            return Lambda.Compile();
        }

        /// <summary>
        /// Creates a void method
        /// </summary>
        /// <param name="target">Target parameter</param>
        /// <param name="params">Params parameter</param>
        /// <param name="methodCall">Method call</param>
        /// <returns>The result</returns>
        private static Func<TTarget?, object?[]?, object?> CreateVoidMethod(ParameterExpression target, ParameterExpression @params, Expression methodCall)
        {
            var Lambda = Expression.Lambda<Action<TTarget?, object?[]?>>(methodCall, target, @params);
            Action<TTarget?, object?[]?> Action = Lambda.Compile();
            return (target, @params) =>
            {
                Action(target, @params);
                return null;
            };
        }
    }
}