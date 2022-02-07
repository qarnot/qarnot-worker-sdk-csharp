namespace QarnotSDK.Worker
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The base class all the services intended to be run as Qarnot workers have to implement.
    /// </summary>
    public abstract class Worker
    {
        /// <summary>
        /// The main logic of the worker.
        /// </summary>
        /// <remarks>
        /// Once the <c>RunAsync</c> function returns (due to an exception being thrown or a regular return), the
        /// worker is shut down. <br />
        /// It is expected for the implementation of <c>RunAsync</c> to handle <c>ct</c> being cancelled
        /// and to return in that case.
        /// </remarks>
        public abstract Task RunAsync(CancellationToken ct);

        /// <summary>
        /// This function is called whenever the service is being shutdown, allowing the
        /// the worker to clean things up and gracefully exit.
        /// </summary>
        /// <remarks>
        /// This function is guaranteed to be called when a worker is being shutdown, be it because <c>RunAsync</c> returned
        /// or the instance is being terminated by the Qarnot runtime. <br />
        /// <c>ct.IsCancellationRequested</c> indicates whether the service is being terminated (<c>true</c>) or <c>RunAsync</c>
        /// exited by itself (<c>false</c>).
        /// </remarks>
        public virtual Task OnStopAsync(CancellationToken ct) =>
            Task.CompletedTask;
    }
}
