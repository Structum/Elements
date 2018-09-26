using System;

namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Provides Information about the Executing Environment for the running application. The information is collected and loaded
    ///     on-demand.
    /// </summary>
    /// <example>
    ///     To get information on the current application (Application Name in this case):
    ///     <code>
    ///     var applicationName = ExecutingEnvironment.CurrentApplication.Name;
    ///     Console.WriteLine("Application Name: {applicationName}");
    ///     </code>
    ///     To see what information is collected for the application see: <see cref="ApplicationInfo"/>.
    ///     <br />
    ///     <br />
    ///     To get information on Local Environment (Operating System in this case):
    ///     <code>
    ///     var os = ExecutingEnvironment.LocalPlatform.OsPlatform;
    ///     Console.WriteLine(Enum.GetName(typeof(OsPlatformType), os));
    ///     </code>
    ///     To see what information is collected for the local platform see: <see cref="PlatformInfo"/>.
    /// </example>
    public static class ExecutingEnvironment
    {
        /// <summary>
        ///     Internal Current Application Info instance.
        /// </summary>
        private static readonly Lazy<ApplicationInfo> LazyCurrentApplication = new Lazy<ApplicationInfo>(ApplicationInfoFactory.CreateCurrentApplicationInfo);

        /// <summary>
        ///     Provides information about the Current Executing Application.
        /// </summary>
        /// <value>An Application Information instance containing the information for current executing application.</value>
        public static ApplicationInfo CurrentApplication => LazyCurrentApplication.Value;

        /// <summary>
        /// Internal Local Platform Info instance.
        /// </summary>
        private static readonly Lazy<PlatformInfo> LazyLocalPlatform = new Lazy<PlatformInfo>(PlatformInfoFactory.CreateLocalPlatformInfo);

        /// <summary>
        ///     Provides information about the local platform where the application is executing. (e.g. OS, Framework Version, etc.)
        /// </summary>
        /// <value>A Platform Information instance containing the information for local platform.</value>
        public static PlatformInfo LocalPlatform => LazyLocalPlatform.Value;
    }
}