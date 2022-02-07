namespace QarnotSDK.Worker
{
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class Worker
    {
        public abstract Task RunAsync(CancellationToken ct);

        public virtual Task OnStopAsync(CancellationToken ct) =>
            Task.CompletedTask;
    }
}
