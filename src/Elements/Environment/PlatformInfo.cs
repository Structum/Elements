namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Defines the Platform Information class.
    /// </summary>
    public class PlatformInfo
    {
        /// <summary>
        ///     Gets/Sets the OS Platform Type.
        /// </summary>
        public OsPlatformType OsPlatform { get; set; }

        /// <summary>
        ///     Gets the Framework Type.
        /// </summary>
        public FrameworkType Framework { get; set; }

        /// <summary>
        ///     Gets the Processor Architecture.
        /// </summary>
        public ProcessorArchitectureType ProcessorArchitecture { get; set; }
    }
}