namespace QarnotSDK.Worker.Internal
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The interface a worker runtime has to follow in order to be loaded.
    /// </summary>
    /// <remarks>
    /// This class is for internal use only and should not be used directly.
    /// </remarks>
    public interface IRuntime
    {
        /// <summary>
        /// Configure the options used to run the workers.
        /// </summary>
        void Configure(Action<Options> configure);
        /// <summary>
        /// Register a worker of type TWorker to be ran by the runtime.
        /// </summary>
        void RegisterWorker<TWorker>(Func<IContext, TWorker> factory) where TWorker : Worker;
        /// <summary>
        /// Start the runtime and its registered worker.
        /// </summary>
        Task RunAsync(CancellationToken ct);
    }
}
