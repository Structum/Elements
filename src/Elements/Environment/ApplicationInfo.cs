namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Defines the Application Information class.
    /// </summary>
    public class ApplicationInfo
    {
        /// <summary>
        ///     Gets/Sets the application name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets/Sets the application version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Gets/Sets the application company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        ///     Gets/Sets the application file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        ///     Gets/Sets the application executing directory.
        /// </summary>
        public string ExecutingDirectory { get; set; }

        /// <summary>
        ///     Gets/Sets the application command line arguments.
        /// </summary>
        public string[] CommandArguments { get; set; }
    }
}