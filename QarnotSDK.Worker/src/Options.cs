namespace QarnotSDK.Worker
{
    public enum RuntimeVersion
    {
        PRIVATE_ALPHA,
    }

    /// <summary>
    /// The configuration options of the runtime in which the worker will be run.
    /// </summary>
    /// <remarks>
    /// The options can be configured using the ASP configuration mechanism. They
    /// live in the <c>Qarnot:Worker</c> section and can be modified with (from lower to higher precedence):
    /// <list type="number">
    ///   <item>
    ///     <description>
    ///       using an <c>appsettings.json</c> file
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       using environment variables prefixed with <c>Qarnot__Worker__</c>
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       using the <see cref="QarnotSDK.Worker.Runtime.Configure(System.Action{Options})">Runtime.Configure(Action&lt;Options&gt;)</see>
    ///       method.
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    /// <seealso href="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration">Configuration in ASP.NET Core</seealso>
    public class Options
    {
        public const string Section = "Qarnot:Worker";

        /// <summary>
        /// The runtime version expected to be used when running the worker code on
        /// the Qarnot platform.
        /// </summary>
        public RuntimeVersion RuntimeVersion { get; set; } = RuntimeVersion.PRIVATE_ALPHA;
    }
}
