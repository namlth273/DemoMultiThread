using System;
using System.Threading.Tasks;

namespace DemoMultiThread
{
    public interface IRequestBuilder
    {
        Task Build();
    }

    public class RequestBuilder : IRequestBuilder
    {
        private readonly string _instanceId = Guid.NewGuid().ToString();
        private readonly ILogTrace _logTrace;

        public RequestBuilder(ILogTrace logTrace)
        {
            _logTrace = logTrace;
        }

        public Task Build()
        {
            _logTrace.Log($"{_instanceId} Building Http request...");
            return Task.CompletedTask;
        }
    }
}