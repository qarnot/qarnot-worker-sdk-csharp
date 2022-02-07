namespace QarnotSDK.Worker.Internal
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    using Q = QarnotSDK.Worker;

    public sealed class LocalServiceAdapter : BackgroundService
    {
        private readonly Worker Worker;

        public LocalServiceAdapter(Worker worker, IOptions<Q.Options> options)
        {
            Worker = worker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) =>
            await Worker.RunAsync(stoppingToken);

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await Worker.OnStopAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }
    }
}
