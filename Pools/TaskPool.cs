using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DemoMultiThread.Pools
{
    public class TaskPool<T> : ITaskPool<T>
    {
        private List<T> _items;
        private BroadcastBlock<T> _firstBlock;
        private int _count = 1;

        public void Init(List<T> items, Func<T, Task> delegateTask)
        {
            _items = items;

            var linkOptions = new DataflowLinkOptions
            {
                PropagateCompletion = true
            };

            _firstBlock = new BroadcastBlock<T>(m => m);

            var mainBlock = new TransformBlock<T, T>(async m =>
            {
                await delegateTask(m);

                return m;
            }, new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 6
            });

            var bufferBlock = new BufferBlock<T>(new DataflowBlockOptions
            {
                BoundedCapacity = 1
            });

            var endBlock = new ActionBlock<T>(m =>
            {
                if (_count == _items.Count) _firstBlock.Complete();
                _count++;
            });

            _firstBlock.LinkTo(mainBlock, linkOptions);

            mainBlock.LinkTo(bufferBlock, linkOptions);

            bufferBlock.LinkTo(endBlock, linkOptions);
        }

        public async Task Run(List<T> items, Func<T, Task> delegateTask)
        {
            Init(items, delegateTask);

            foreach (var item in _items)
            {
                _firstBlock.Post(item);
            }

            await _firstBlock.Completion;
        }
    }
}