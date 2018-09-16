using System;

namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Provides Information about the Executing Environment for running application.
    /// </summary>
    public static class ExecutingEnvironment
    {
        /// <summary>
        ///     Internal Current Application Info instance.
        /// </summary>
        private static readonly Lazy<ApplicationInfo> LazyCurrentApplication = new Lazy<ApplicationInfo>(ApplicationInfoFactory.CreateCurrentApplicationInfo);

        /// <summary>
        ///     Provides information about the Current Application.
        /// </summary>
        public static ApplicationInfo CurrentApplication => LazyCurrentApplication.Value;

        /// <summary>
        /// Internal Local Platform Info instance.
        /// </summary>
        private static readonly Lazy<PlatformInfo> LazyLocalPlatform = new Lazy<PlatformInfo>(PlatformInfoFactory.CreateLocalPlatformInfo);

        /// <summary>
        ///     Provides information about the local platform where the application is executing. (e.g. OS, Framework Version, etc.)
        /// </summary>
        public static PlatformInfo LocalPlatform => LazyLocalPlatform.Value;
    }
}