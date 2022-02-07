namespace QarnotSDK.Worker.Internal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRuntime
    {
        void Configure(Action<Options> configure);
        void RegisterWorker<TWorker>(Func<IContext, TWorker> factory) where TWorker : Worker;
        Task RunAsync(CancellationToken ct);
    }
}
