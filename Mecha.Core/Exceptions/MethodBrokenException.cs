using System;

namespace Mecha.Core.Exceptions
{
    /// <summary>
    /// The method threw an exception
    /// </summary>
    /// <seealso cref="Exception"/>
    public class MethodBrokenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodBrokenException"/> class.
        /// </summary>
        public MethodBrokenException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodBrokenException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MethodBrokenException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodBrokenException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <see
        /// langword="Nothing"/> in Visual Basic) if no inner exception is specified.
        /// </param>
        public MethodBrokenException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}