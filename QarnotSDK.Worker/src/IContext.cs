namespace QarnotSDK.Worker
{
    /// <summary>
    /// An aggregation of parameters defining the context in which a worker will be run.
    /// </summary>
    /// <remarks>
    /// An <c>IContext</c> is given to the user-supplied factory creating the Worker instance.
    /// This allows tweaking the worker depending on the context it will be run in.
    /// </remarks>
    public interface IContext
    {
        /// <summary>
        /// Whether the worker will be run on the Qarnot platform or locally with the
        /// fallback runtime.
        /// </summary>
        bool IsRunningLocally { get; }
    }
}
