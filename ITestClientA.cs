using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoMultiThread
{
    public interface ITestClientA
    {
        Task Get();
    }

    public class TestClientA : ITestClientA
    {
        private readonly HttpClient _httpClient;
        private readonly ILogTrace _logTrace;
        private readonly Func<IRequestBuilder> _requestBuilder;

        public TestClientA(HttpClient httpClient, ILogTrace logTrace, Func<IRequestBuilder> requestBuilder)
        {
            _httpClient = httpClient;
            _logTrace = logTrace;
            _requestBuilder = requestBuilder;
        }

        public async Task Get()
        {
            _logTrace.Log("Getting...");
            await _requestBuilder().Build();
            Console.WriteLine("Get... Received...");
            _logTrace.Log("Received...");
        }
    }
}