namespace QarnotSDK.Worker.Internal
{

    /// <summary>
    /// A definition of the context passed to the Worker factory when the code is
    /// run locally.
    /// </summary>
    /// <remarks>
    /// This class is for internal use only and should not be used directly.
    /// </remarks>
    internal sealed class LocalContext : IContext
    {
        public bool IsRunningLocally { get => true; }
    }
}
