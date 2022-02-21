namespace QarnotSDK.Worker.Internal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Local runtime used as a fallback when the Qarnot runtime is not configured to
    /// be found.
    /// </summary>
    /// <remarks>
    /// This class is for internal use only and should not be used directly.
    /// </remarks>
    internal sealed class LocalRuntime : IRuntime
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

        public async Task RunAsync(CancellationToken ct)
        {
            var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(ct);
            await HostBuilder
                .ConfigureServices(services =>
                {
                    services.AddSingleton<BoxedCancellationToken>(new BoxedCancellationToken(ct));
                    services.AddSingleton<CancellationTokenSource>(cancellationSource);
                })
                .Build()
                .RunAsync(cancellationSource.Token);
        }

        public void Configure(Action<Options> configure) =>
            HostBuilder.ConfigureServices(services =>
            {
                services.PostConfigure(configure);
            });
    }
}
