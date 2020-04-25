using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DemoMultiThread
{
    public class TestHttpClientHandler : DelegatingHandler
    {
        private readonly ILogTrace _logTrace;

        public TestHttpClientHandler(ILogTrace logTrace)
        {
            _logTrace = logTrace;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _logTrace.Log("Inside HttpClientHandler...");
            _logTrace.Flush();
            return base.SendAsync(request, cancellationToken);
        }
    }
}