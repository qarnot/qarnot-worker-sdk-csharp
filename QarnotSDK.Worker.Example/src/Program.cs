namespace QarnotSDK.Worker.Example
{
    using Autofac;
    using Serilog;
    using AutofacSerilogIntegration;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Q = QarnotSDK.Worker;

    public static class Program
    {
        public static async Task Main()
        {
            Q.Runtime.RegisterWorker(context =>
            {
                var builder = new ContainerBuilder();

                builder.RegisterLogger(
                    new LoggerConfiguration()
                        .WriteTo.Console()
                        .CreateLogger()
                );

                builder.RegisterInstance<IContext>(context);
                builder.RegisterType<Worker>().As<Q.Worker>();

                return builder.Build().Resolve<Q.Worker>();
            });

            await Q.Runtime.RunAsync(
                new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token
            );
        }
    }
}
