using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Q = QarnotSDK.Worker;
using System.Reflection;

namespace QarnotSDK.Worker.Tests;

[TestFixture, NonParallelizable]
public class Tests
{
    [SetUp]
    public void SetUp()
    {
        // Because we can only register one worker _and_ the `Runtime` class is static,
        // we have to resort to this monstruosity.
        var assembly = typeof(Q.Runtime).Assembly;
        var loaderType = assembly.GetType("QarnotSDK.Worker.Internal.RuntimeLoader");
        var loaderCtor = loaderType!.GetConstructors()[0];
        var loader = loaderCtor!.Invoke(null);
        var loadingMethod =  loaderType.GetMethod("Load", Type.EmptyTypes);

        var runtime = typeof(Q.Runtime);
        var actualRuntime = runtime.GetField("_actualRuntime", BindingFlags.NonPublic | BindingFlags.Static);

        actualRuntime!.SetValue(null, loadingMethod!.Invoke(loader, null));
    }

    [Test]
    public async Task TestShortRunning()
    {
        Q.Runtime.RegisterWorker(_ => new ShortRunningWorker());
        await Q.Runtime.RunAsync();
    }

    [Test]
    public async Task TestLongRunning()
    {
        List<int> list = new();
        List<int> expected = new List<int> { 1 };
        Q.Runtime.RegisterWorker(_ => new LongRunningWorker(list));


        var before = DateTime.Now;

        await Q.Runtime.RunAsync(
            // Stop processing after 1 second.
            new CancellationTokenSource(TimeSpan.FromSeconds(1)).Token
        );

        var after = DateTime.Now;

        // The cancellation token did stop the processing in ~ 1 sec.
        Assert.That(after, Is.LessThan(before + TimeSpan.FromSeconds(5)));

        // Only one item has been inserted.
        Assert.That(list, Is.EquivalentTo(expected));
    }
}

internal class ShortRunningWorker : Q.Worker
{
    public override Task RunAsync(CancellationToken ct)
    {
        return Task.CompletedTask;
    }

    public override Task OnStopAsync(CancellationToken ct)
    {
        // Check that RunAsync is said to have exited without
        // cancellation.
        Assert.That(ct.IsCancellationRequested, Is.False);
        return Task.CompletedTask;
    }
}


internal class LongRunningWorker : Q.Worker
{
    private readonly IList<int> List;

    public LongRunningWorker(IList<int> list)
    {
        List = list;
    }

    public override async Task RunAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            List.Add(1);
            await Task.Delay(TimeSpan.FromSeconds(20), ct);
        }
    }

    public override Task OnStopAsync(CancellationToken ct)
    {
        // Check that the cancellation has indeed been requested.
        Assert.That(ct.IsCancellationRequested, Is.True);
        return Task.CompletedTask;
    }
}
