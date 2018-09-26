namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Defines the Application Information class, which can be used to collect information
    ///     about applications on a device.
    /// </summary>
    /// <example>
    ///     The following code demonstrates how to create an ApplicationInfo instance:
    ///     <code>
    ///     var photoshopAppInfo = new ApplicationInfo {
    ///         Name = "Photoshop.exe",
    ///         Version = "7.0.0.354",
    ///         CompanyName = "Adobe, Inc.",
    ///         FilePath = "C:\\Program Files\\Adobe Photoshop\\Photoshop.exe",
    ///         ExecutingDirectory = "C:\\Program Files\\Adobe Photoshop\\",
    ///         CommandArguments = new string{"-c"}
    ///     };
    ///     </code>
    ///     <br />
    ///     You can create the ApplicationInfo for the running application using the <see cref="PlatformInfoFactory"/>:
    ///     <code>
    ///     var thisAppInfo = PlatformInfoFactory.CreateCurrentApplicationInfo();
    ///     </code>
    /// </example>
    public class ApplicationInfo
    {
        /// <summary>
        ///     Gets or Sets the application name.
        /// </summary>
        /// <value>A string containing the name of the application file. (e.g. Photoshop.exe)(</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or Sets the application version.
        /// </summary>
        /// <value>A string containing the application version. (e.g. 1.0.0.0)</value>
        public string Version { get; set; }

        /// <summary>
        ///     Gets or Sets the application company name.
        /// </summary>
        /// <value>A string containing the Company Name that owns the application. (e.g. Adobe, Inc.)</value>
        public string CompanyName { get; set; }

        /// <summary>
        ///     Gets or Sets the application file path.
        /// </summary>
        /// <value>A string containing the full application path. (e.g. C:\Program Files\Adobe Photoshop\Photoshop.exe)</value>
        public string FilePath { get; set; }

        /// <summary>
        ///     Gets or Sets the application executing directory.
        /// </summary>
        /// <value>A string containing the Executing directory for the application. (e.g. C:\\Program Files\\Adobe Photoshop\\")</value>
        public string ExecutingDirectory { get; set; }

        /// <summary>
        ///     Gets or Sets the application command line arguments.
        /// </summary>
        /// <value>A string containing command-line arguments. (e.g. -c)</value>
        public string[] CommandArguments { get; set; }
    }
}