namespace QarnotSDK.Worker.Internal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;

    public sealed class LocalRuntime : IRuntime
    {
        private bool AlreadyRegisteredWorker = false;
        private readonly IHostBuilder HostBuilder;

        public LocalRuntime()
        {
            HostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices((ctx, services) =>
                {
                    services
                        .Configure<Options>(
                            ctx.Configuration.GetSection(Options.Section)
                        )
                        .AddLogging(logging =>
                        {
                            logging.ClearProviders();
                        });
                });
        }

        public void RegisterWorker<TWorker>(Func<IContext, TWorker> factory)
            where TWorker : Worker
        {
            if (AlreadyRegisteredWorker)
            {
                throw new Exception("only a single worker can be registered");
            }

            HostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<Worker>(_ => factory(new LocalContext()));
                services.AddHostedService<LocalServiceAdapter>();
            });

            AlreadyRegisteredWorker = true;
        }

        public async Task RunAsync(CancellationToken ct) =>
            await HostBuilder.Build().RunAsync(ct);

        public void Configure(Action<Options> configure) =>
            HostBuilder.ConfigureServices(services =>
            {
                services.PostConfigure(configure);
            });
    }
}
