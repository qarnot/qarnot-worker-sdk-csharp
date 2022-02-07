using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Q = QarnotSDK.Worker;

namespace QarnotSDK.Worker.Tests;

public class Tests
{
    [Test]
    public async Task TestSimpleWorker()
    {
        List<int> list = new();
        List<int> expected = new List<int> { 1 };
        Q.Runtime.RegisterWorker<Worker>(_ => new Worker(list));


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

internal class Worker : Q.Worker
{
    private readonly IList<int> List;

    public Worker(IList<int> list)
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
}
