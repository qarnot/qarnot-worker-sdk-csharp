namespace QarnotSDK.Worker.Internal
{
    public sealed class LocalContext : IContext
    {
        public bool IsRunningLocally { get => true; }
    }
}
