using System;
using System.Collections.Generic;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Utils extensions
    /// </summary>
    public static class UtilsExtensions
    {
        /// <summary>
        /// The methods
        /// </summary>
        private static readonly Dictionary<Type, Func<object, bool>> Methods = new()
        {
            [typeof(float)] = value => !float.IsFinite((float)value),
            [typeof(float?)] = value => !float.IsFinite((float)value),
            [typeof(double)] = value => !double.IsFinite((double)value),
            [typeof(double?)] = value => !double.IsFinite((double)value),
        };

        /// <summary>
        /// Gets the underlying array type if it exists
        /// </summary>
        /// <param name="type">Type to check.</param>
        /// <returns>The underlying array type if it exists, null otherwise.</returns>
        public static Type? GetUnderlyingArrayType(this Type? type)
        {
            if (type is null)
                return null;
            if (type.IsSpecialType(out _))
            {
                Type? ParameterElementType = type.GetGenericArguments()[0];
                return ParameterElementType.MakeArrayType();
            }
            return type.IsArray ? type : null;
        }

        /// <summary>
        /// Determines whether the specified type is an array segment.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if it is an array segment, false otherwise.</returns>
        public static bool IsArraySegment(this Type? type)
        {
            if (type is null)
                return false;

            // Check if the type is a reference type
            if (!type.IsByRef)
                return false;
            // Get the underlying type of the reference type
            Type? ElementType = type.GetElementType();

            // Check if the underlying type is generic
            if ((ElementType?.IsGenericType) != true)
                return false;

            // Check if the generic type definition is ArraySegment<>
            return ElementType.GetGenericTypeDefinition() == typeof(ArraySegment<>);
        }

        /// <summary>
        /// Determines whether the specified value is infinite.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is infinite; otherwise, <c>false</c>.</returns>
        public static bool IsInfinite(this object? value)
        {
            return value is not null
                && Methods.TryGetValue(value.GetType(), out Func<object, bool>? Result)
                && Result(value);
        }

        /// <summary>
        /// Determines whether the specified type is a Memory.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if it is a Memory, false otherwise.</returns>
        public static bool IsMemory(this Type? type) => type?.IsGenericType == true && type.GetGenericTypeDefinition() == typeof(Memory<>);

        /// <summary>
        /// Determines whether the specified type is a nullable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if it is a nullable, false otherwise.</returns>
        public static bool IsNullable(this Type? type) => type?.IsGenericType == true && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        /// <summary>
        /// Determines whether the specified type is a ReadOnlyMemory.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if it is a ReadOnlyMemory, false otherwise.</returns>
        public static bool IsReadOnlyMemory(this Type? type) => type?.IsGenericType == true && type.GetGenericTypeDefinition() == typeof(ReadOnlyMemory<>);

        /// <summary>
        /// Determines whether the specified type is a ReadOnlySpan.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if it is a ReadOnlySpan, false otherwise.</returns>
        public static bool IsReadOnlySpan(this Type? type) => type?.IsByRefLike == true && type.GetGenericTypeDefinition() == typeof(ReadOnlySpan<>);

        /// <summary>
        /// Determines whether the specified type is a Span.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if it is a Span, false otherwise.</returns>
        public static bool IsSpan(this Type? type) => type?.IsByRefLike == true && type.GetGenericTypeDefinition() == typeof(Span<>);

        /// <summary>
        /// Determines whether the specified type is one of the special types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="specialType">The special type.</param>
        /// <returns>True if it is a special case type, false otherweise.</returns>
        public static bool IsSpecialType(this Type? type, out Type? specialType)
        {
            if (type is null)
            {
                specialType = null;
                return false;
            }
            if (type.IsReadOnlyMemory())
            {
                specialType = typeof(ReadOnlyMemory<>);
                return true;
            }
            if (type.IsMemory())
            {
                specialType = typeof(Memory<>);
                return true;
            }
            if (type.IsReadOnlySpan())
            {
                specialType = typeof(ReadOnlySpan<>);
                return true;
            }
            if (type.IsSpan())
            {
                specialType = typeof(Span<>);
                return true;
            }
            if (type.IsArraySegment())
            {
                specialType = typeof(ArraySegment<>);
                return true;
            }
            specialType = null;
            return false;
        }

        /// <summary>
        /// Determines whether the specified type is a struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True if it is a struct, false otherwise.</returns>
        public static bool IsStruct(this Type? type) => type?.IsValueType == true && !type.IsPrimitive && type != typeof(decimal) && type != typeof(DateTime) && !type.IsEnum;
    }
}