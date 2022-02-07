namespace QarnotSDK.Worker.Internal;

using System;
using System.Threading;
using System.Threading.Tasks;
using QarnotSDK.Worker;

public class DummyInternalRuntime : IRuntime
{
    private Worker? Worker;

    public void Configure(Action<Options> configure)
    {
        // Empty.
    }

    public void RegisterWorker<TWorker>(Func<IContext, TWorker> factory)
        where TWorker : Worker
    {
        Worker = factory(new Context());
    }

    public async Task RunAsync(CancellationToken ct)
    {
        Console.WriteLine(typeof(DummyInternalRuntime).FullName);
        try
        {
            await Worker!.RunAsync(ct);
        }
        finally
        {
            await Worker!.OnStopAsync(ct);
        }
    }
}

internal class Context : IContext
{
    public bool IsRunningLocally => true;
}
