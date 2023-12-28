using Mecha.Core.Generator.Helpers;
using Mecha.Core.Generator.Interfaces;
using System.Net.Http;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// HttpClient Generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class HttpClientGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileStreamGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public HttpClientGenerator(Mirage.Random random)
        {
            RandomObj = random;
            Clients = new HttpClient[]
            {
                new FaultyHttpClient(),
                new EmptyHttpClient(random)
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

        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        private HttpClient[] Clients { get; }

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
                && typeof(HttpClient).IsAssignableFrom(parameter.ParameterType);
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The next object.</returns>
        public ParameterValue? Next(ParameterInfo? parameter, object? min, object? max) => new("HttpClient Generator", RandomObj.Next(Clients));
    }
}