namespace QarnotSDK.Worker.Internal
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    using Q = QarnotSDK.Worker;

    /// <summary>
    /// Adapt a Qarnot Worker into an ASP hostable service.
    /// </summary>
    /// <remarks>
    /// This class is for internal use only and should not be used directly.
    /// </remarks>
    internal sealed class LocalServiceAdapter : BackgroundService
    {
        private readonly Worker Worker;
        private readonly CancellationTokenSource CancellationSource;
        private readonly BoxedCancellationToken OriginalToken;

        public LocalServiceAdapter(
            Worker worker,
            IOptions<Q.Options> options,
            CancellationTokenSource cancellationSource,
            BoxedCancellationToken originalToken
        )
        {
            Worker = worker;
            CancellationSource = cancellationSource;
            OriginalToken = originalToken;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await Worker.RunAsync(stoppingToken);
            }
            finally
            {
                CancellationSource.Cancel();
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Worker.OnStopAsync(OriginalToken.CancellationToken);
            }
            finally
            {
                await base.StopAsync(cancellationToken);
            }
        }
    }
}
