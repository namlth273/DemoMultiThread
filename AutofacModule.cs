using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DemoMultiThread
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var services = new ServiceCollection();

            services.AddHttpClient<ITestClientA, TestClientA>((serviceProvider, client) =>
            {
                client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            }).AddHttpMessageHandler<TestHttpClientHandler>();

            builder.Populate(services);
            builder.RegisterType<LogTrace>().As<ILogTrace>().InstancePerLifetimeScope();
            builder.RegisterType<TestService>().As<ITestService>();
            builder.RegisterType<TestServiceA>().As<ITestServiceA>();
            builder.RegisterType<RequestBuilder>().As<IRequestBuilder>();
            builder.RegisterType<TestHttpClientHandler>();
        }
    }
}