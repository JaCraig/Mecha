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
            Streams = new FileStream[]
            {
                new EmptyFileStream(),
                new FaultyFileStream()
            };
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

        private FileStream[] Streams { get; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo? parameter)
        {
            return parameter?.HasDefaultValue == false
                && typeof(FileStream).IsAssignableFrom(parameter.ParameterType);
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The next object.</returns>
        public object? Next(ParameterInfo? parameter, object? min, object? max) => RandomObj.Next(Streams);
    }
}