namespace QarnotSDK.Worker.Internal
{
    using System.Threading;

    public sealed class BoxedCancellationToken
    {
        public CancellationToken CancellationToken { get; }

        public BoxedCancellationToken(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
        }
    }
}
