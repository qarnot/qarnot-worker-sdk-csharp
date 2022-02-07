namespace QarnotSDK.Worker.Tests;

using System.Threading;
using System.Threading.Tasks;
using Q = QarnotSDK.Worker;

public static class Program
{
	public static async Task Main()
	{
		Q.Runtime.RegisterWorker<Worker>(_ => new Worker());
		await Q.Runtime.RunAsync();
	}
}

internal class Worker : Q.Worker
{
    public override Task RunAsync(CancellationToken ct)
    {
		return Task.CompletedTask;
    }
}
