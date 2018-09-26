using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Provides factory methods for the <see cref="PlatformInfoFactory"/> class.
    /// </summary>
    /// <example>
    ///     To collect the local platform information just call the <see cref="CreateLocalPlatformInfo"/> Factory method:
    ///     <code>
    ///     var localPlatformInfo = PlatformInfoFactory.CreateLocalPlatformInfo();
    ///     </code>
    /// </example>
    public static  class PlatformInfoFactory
    {
        /// <summary>
        ///     Creates the Local Platform Information.
        /// </summary>
        /// <returns>Local Platform Information.</returns>
        public static PlatformInfo CreateLocalPlatformInfo()
        {
            PlatformInfo localPlatform = new PlatformInfo {
                OsPlatform = GetOsPlatform(),
                Framework = GetFramework(),
                ProcessorArchitecture = GetProcessorArchitecture()
            };

            return localPlatform;
        }

        /// <summary>
        ///     Returns the OS Platform.
        /// </summary>
        /// <returns>OS Platform.</returns>
        private static OsPlatformType GetOsPlatform()
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                return OsPlatformType.Linux;
            }

            PlatformID platform = System.Environment.OSVersion.Platform;

            if ((int) platform == 128) {
                platform = PlatformID.Unix;
            }

            switch (platform) {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return OsPlatformType.Windows;
                case PlatformID.Unix:
                    return OsPlatformType.Unix;
                case PlatformID.MacOSX:
                    return OsPlatformType.MacOs;
                default:
                    return OsPlatformType.Unknown;
            }
        }

        /// <summary>
        ///     Gets the Framework Type.
        /// </summary>
        /// <returns>Framework Type.</returns>
        private static FrameworkType GetFramework()
        {
            if (Type.GetType("Mono.Runtime") != null) {
                return FrameworkType.Mono;
            }

            var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
            var assemblyPath = assembly.CodeBase.Split(new[] {'/', '\\'}, StringSplitOptions.RemoveEmptyEntries);
            int netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");

            return netCoreAppIndex > 0 ? FrameworkType.DotNetCore : FrameworkType.DotNet;
        }

        /// <summary>
        ///     Returns the Processor Architecture supported by the Operating System.
        /// </summary>
        /// <returns>Processor Architecture.</returns>
        private static ProcessorArchitectureType GetProcessorArchitecture()
        {
            return IntPtr.Size == 8 ? ProcessorArchitectureType.x64 : ProcessorArchitectureType.x86;
        }
    }
}