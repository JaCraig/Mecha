using Mecha.Core.Generator.Helpers;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests.Generator.Helpers
{
    public class FaultyHttpClientTests
    {
        public FaultyHttpClientTests()
        {
            _TestClass = new FaultyHttpClient();
        }

        private readonly FaultyHttpClient _TestClass;

        [Fact]
        public async Task SendAsyncThrowsHttpRequestException() => await Assert.ThrowsAsync<HttpRequestException>(() => _TestClass.SendAsync(new HttpRequestMessage(), CancellationToken.None));
    }
}