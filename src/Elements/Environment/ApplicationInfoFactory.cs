using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Provides factory methods for the <see cref="ApplicationInfo"/> class.
    /// </summary>
    /// <example>
    ///     To collect the current application information just call the <see cref="CreateCurrentApplicationInfo"/> Factory method:
    ///     <code>
    ///     var thisAppInfo = ApplicationInfoFactory.CreateCurrentApplicationInfo();
    ///     </code>
    /// </example>
    public static class ApplicationInfoFactory
    {
        /// <summary>
        ///     Creates and returns the Current Application information.
        /// </summary>
        /// <remarks>
        ///     This method can gather the information for the Current Executing Application if it is a managed .Net application; when this is not the case this method will
        ///     return an application information class with empty values.
        /// </remarks>
        /// <returns>Current Application Information.</returns>
        public static ApplicationInfo CreateCurrentApplicationInfo()
        {
            Assembly asm = Assembly.GetEntryAssembly();

            if (asm == null) {
                return new ApplicationInfo {
                    ExecutingDirectory = System.AppDomain.CurrentDomain.BaseDirectory
                };
            }

            string name = Path.GetFileName(System.Environment.GetCommandLineArgs()[0]);
            return new ApplicationInfo {
                Name = name,
                Version = asm.GetName().Version.ToString(),
                ExecutingDirectory =  System.AppDomain.CurrentDomain.BaseDirectory,
                CommandArguments = System.Environment.GetCommandLineArgs(),
                CompanyName = FileVersionInfo.GetVersionInfo(asm.Location).CompanyName,
                FilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, name ?? "")
            };
        }
    }
}