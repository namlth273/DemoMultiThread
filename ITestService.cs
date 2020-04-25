using System;
using System.Threading.Tasks;

namespace DemoMultiThread
{
    public interface ITestService
    {
        Task DoTask();
    }

    public class TestService : ITestService
    {
        private readonly ILogTrace _logTrace;
        private readonly ITestServiceA _serviceA;

        public TestService(ILogTrace logTrace, ITestServiceA serviceA)
        {
            _logTrace = logTrace;
            _serviceA = serviceA;
        }

        public async Task DoTask()
        {
            await Task.Delay(1000);
            _logTrace.Log(DateTime.Now.ToLongTimeString());
            await _serviceA.DoTask();
        }
    }

    public interface ITestServiceA
    {
        Task DoTask();
    }

    public class TestServiceA : ITestServiceA
    {
        private readonly ILogTrace _logTrace;

        public TestServiceA(ILogTrace logTrace)
        {
            _logTrace = logTrace;
        }

        public async Task DoTask()
        {
            await Task.Delay(1000);
            _logTrace.Log(DateTime.Now.ToLongTimeString());
        }
    }
}