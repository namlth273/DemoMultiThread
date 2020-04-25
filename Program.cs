using Autofac;
using System;
using System.Threading.Tasks;

namespace DemoMultiThread
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var builder = new ContainerBuilder();

            builder.RegisterModule<AutofacModule>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var service = scope.Resolve<ITestService>();

                await service.DoTask();

                var logTrace = scope.Resolve<ILogTrace>();

                var clientA = scope.Resolve<ITestClientA>();

                await clientA.Get();

                var clientB = scope.Resolve<ITestClientA>();

                await clientB.Get();

                logTrace.Flush();
            }

            //var items = new List<int>();

            //for (int i = 0; i < 30; i++)
            //{
            //    items.Add(i);
            //}

            //var taskPool = new TaskPool<int>().Run(items, async i =>
            //{
            //    await Task.Delay(500);
            //    Console.WriteLine(i);
            //});

            //await taskPool;

            Console.WriteLine("Done!");
        }
    }

    public class Message
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}