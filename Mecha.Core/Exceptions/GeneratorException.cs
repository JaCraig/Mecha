﻿using System;

namespace Mecha.Core.Exceptions
{
    /// <summary>
    /// Generator exception
    /// </summary>
    /// <seealso cref="Exception"/>
    public class GeneratorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorException"/> class.
        /// </summary>
        public GeneratorException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GeneratorException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing
        /// in Visual Basic) if no inner exception is specified.
        /// </param>
        public GeneratorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}