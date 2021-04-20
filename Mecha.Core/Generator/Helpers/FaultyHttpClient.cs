using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mecha.Core.Generator.Helpers
{
    /// <summary>
    /// Faulty HTTP Client
    /// </summary>
    /// <seealso cref="HttpClient"/>
    public class FaultyHttpClient : HttpClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FaultyHttpClient"/> class.
        /// </summary>
        public FaultyHttpClient()
            : base(new MessageHandler(), true)
        {
        }

        /// <summary>
        /// Send an HTTP request as an asynchronous operation.
        /// </summary>
        /// <param name="request">The HTTP request message to send.</param>
        /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        /// <exception cref="HttpRequestException">Website not accessible</exception>
        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new HttpRequestException("Website not accessible");
        }

        /// <summary>
        /// Message handler
        /// </summary>
        /// <seealso cref="HttpMessageHandler"/>
        private class MessageHandler : HttpMessageHandler
        {
            /// <summary>
            /// Sends the asynchronous.
            /// </summary>
            /// <param name="request">The request.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns></returns>
            /// <exception cref="HttpRequestException">Website not accessible</exception>
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                throw new HttpRequestException("Website not accessible");
            }
        }
    }
}