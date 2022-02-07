namespace QarnotSDK.Worker.Example
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Serilog;
    using Q = QarnotSDK.Worker;

    public class Worker : Q.Worker
    {
        private readonly IContext Ctx;
        private readonly ILogger Logger;

        public Worker(IContext ctx, ILogger logger)
        {
            Ctx = ctx;
            Logger = logger;
        }

        public override async Task RunAsync(CancellationToken ct)
        {
            for (var i = 0; /* empty */; ++i)
            {
                ct.ThrowIfCancellationRequested();

                Logger.Information("Working: {i}", i);
                await Task.Delay(TimeSpan.FromSeconds(5), ct);
            }
        }

        public override Task OnStopAsync(CancellationToken cancellationToken)
        {
            Logger.Information("Stopping...");
            return Task.CompletedTask;
        }
    }
}
