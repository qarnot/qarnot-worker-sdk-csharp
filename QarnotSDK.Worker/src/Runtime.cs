namespace QarnotSDK.Worker
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using QarnotSDK.Worker.Internal;

    /// <summary>
    /// The gateway to interacting with the Qarnot Worker runtime.
    /// </summary>
    public static class Runtime
    {
        private static IRuntime _actualRuntime;

        private static IRuntime ActualRuntime
        {
            get
            {
                if (_actualRuntime is null)
                {
                    _actualRuntime = new RuntimeLoader().Load();
                }

                return _actualRuntime;
            }
        }

        /// <summary>
        /// Configure the runtime's running options.
        /// </summary>
        /// <seealso cref="QarnotSDK.Worker.Options" />
        public static void Configure(Action<Options> configure) =>
            ActualRuntime.Configure(configure);

        /// <summary>
        /// Register a <see cref="QarnotSDK.Worker.Worker">Worker</see> instance.
        /// </summary>
        /// <remarks>
        /// At the moment <b>only one worker can be registered</b>. All subsequent calls to
        /// <c>RegisterWorker</c> will throw an exception.
        /// </remarks>
        public static void RegisterWorker<TWorker>(Func<IContext, TWorker> factory) where TWorker : Worker =>
            ActualRuntime.RegisterWorker(factory);

        /// <summary>
        /// Start the runtime and its registered <see cref="QarnotSDK.Worker.Worker">Worker</see>.
        /// </summary>
        /// <remarks>
        /// The runtime can be stopped gracefully by cancelling the <c>CancellationToken</c> given to it.
        /// </remarks>
        public static async Task RunAsync(CancellationToken ct = default) =>
            await ActualRuntime.RunAsync(ct);
    }
}
