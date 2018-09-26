namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Defines the Platform Information class, which can be used to collect information
    ///     about platforms on an specific device.
    /// </summary>
    /// <example>
    ///     The following code demonstrates how to create an PlatformInfo instance:
    ///     <code>
    ///     var remoteDevicePlatform = new PlatformInfo {
    ///         OsPlatform = OsPlatformType.Linux,
    ///         Framework = FrameworkType.Mono,
    ///         ProcessorArchitecture = ProcessorArchitectureType.x64
    ///     };
    ///     </code>
    ///     <br />
    ///     You can create the PlatformInfo for the local device using the <see cref="ApplicationInfoFactory"/>:
    ///     <code>
    ///     var localPlatformInfo = ApplicationInfoFactory.CreateLocalPlatformInfo();
    ///     </code>
    /// </example>
    public class PlatformInfo
    {
        /// <summary>
        ///     Gets/Sets the OS Platform Type.
        /// </summary>
        /// <value>OsPlatformType which represents the OS Platform. (e.g. Linux, MacOs)</value>
        public OsPlatformType OsPlatform { get; set; }

        /// <summary>
        ///     Gets the Framework Type.
        /// </summary>
        /// <value>FrameworkType which represents the .Net Framework used. (e.g. Mono, .Net, .Net Core)</value>
        public FrameworkType Framework { get; set; }

        /// <summary>
        ///     Gets the Processor Architecture.
        /// </summary>
        /// <value>ProcessorArchitectureType which represents the supported Processor Architecture. (e.g. x86 for 32-bit and x64 for 63-bit).</value>
        public ProcessorArchitectureType ProcessorArchitecture { get; set; }
    }
}