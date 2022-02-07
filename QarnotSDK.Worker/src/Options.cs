namespace QarnotSDK.Worker
{
    public enum RuntimeVersion
    {
        PRIVATE_ALPHA,
    }

    public class Options
    {
        public const string Section = "Qarnot:Worker";

        public RuntimeVersion RuntimeVersion { get; set; } = RuntimeVersion.PRIVATE_ALPHA;
    }
}
