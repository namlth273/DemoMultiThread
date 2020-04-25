using System;
using System.Collections.Concurrent;
using System.Linq;

namespace DemoMultiThread
{
    public interface ILogTrace
    {
        void Log(string message);
        void Flush();
    }

    public class LogTrace : ILogTrace
    {
        private readonly string _instanceId = Guid.NewGuid().ToString();
        private readonly ConcurrentBag<string> _logEntries = new ConcurrentBag<string>();

        public void Log(string message)
        {
            _logEntries.Add(message);
        }

        public void Flush()
        {
            var reversedList = _logEntries?.Reverse().ToList();

            if (reversedList == null) return;

            foreach (var logEntry in reversedList)
            {
                Console.WriteLine(_instanceId + " " + logEntry);
            }
        }
    }
}