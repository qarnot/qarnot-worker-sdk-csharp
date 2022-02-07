namespace QarnotSDK.Worker
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using QarnotSDK.Worker.Internal;

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

        public static void Configure(Action<Options> configure) =>
            ActualRuntime.Configure(configure);

        public static void RegisterWorker<TWorker>(Func<IContext, TWorker> factory) where TWorker : Worker =>
            ActualRuntime.RegisterWorker(factory);

        public static async Task RunAsync(CancellationToken ct = default) =>
            await ActualRuntime.RunAsync(ct);
    }
}
