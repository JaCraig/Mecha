using Mecha.Core.Generator.Helpers;
using Mecha.Core.Generator.Interfaces;
using System.IO;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// File stream generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class FileStreamGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileStreamGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public FileStreamGenerator(Mirage.Random random)
        {
            RandomObj = random;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => 1;

        /// <summary>
        /// Gets the random object.
        /// </summary>
        /// <value>The random object.</value>
        public Mirage.Random RandomObj { get; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter)
        {
            return !parameter.HasDefaultValue
                && typeof(FileStream).IsAssignableFrom(parameter.ParameterType);
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        public object? Next(ParameterInfo parameter, object? min, object? max)
        {
            if (RandomObj.Next<bool>())
                return new FaultyFileStream();
            return new EmptyFileStream();
        }
    }
}