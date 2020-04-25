using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoMultiThread.Pools
{
    public interface ITaskPool<T>
    {
        Task Run(List<T> items, Func<T, Task> delegateTask);
    }
}