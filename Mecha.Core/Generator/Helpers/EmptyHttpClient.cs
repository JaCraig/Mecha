using Mirage;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mecha.Core.Generator.Helpers
{
    /// <summary>
    /// Empty HTTP Client
    /// </summary>
    /// <seealso cref="HttpClient"/>
    public class EmptyHttpClient : HttpClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyHttpClient"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public EmptyHttpClient(Random random)
            : base(new MessageHandler(random), true)
        {
        }

        /// <summary>
        /// Message handler
        /// </summary>
        /// <seealso cref="HttpMessageHandler"/>
        private class MessageHandler : HttpMessageHandler
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="MessageHandler"/> class.
            /// </summary>
            /// <param name="random">The random.</param>
            public MessageHandler(Random random)
            {
                Random = random;
            }

            /// <summary>
            /// Gets the random.
            /// </summary>
            /// <value>The random.</value>
            public Random Random { get; }

            /// <summary>
            /// Sends the asynchronous.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns></returns>
            /// <exception cref="HttpRequestException">Website not accessible</exception>
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new HttpResponseMessage(Random.Next<System.Net.HttpStatusCode>()));
            }
        }
    }
}