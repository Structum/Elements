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
        /// <returns>Current Application Information.</returns>
        public static ApplicationInfo CreateCurrentApplicationInfo()
        {
            ApplicationInfo currApp = new ApplicationInfo {
                Name = Path.GetFileName(System.Environment.GetCommandLineArgs()[0]),
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                ExecutingDirectory = GetExecutingDirectory(),
                CommandArguments = System.Environment.GetCommandLineArgs(),
                CompanyName = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName
            };
            currApp.FilePath = Path.Combine(currApp.ExecutingDirectory, currApp.Name ?? "");

            return currApp;
        }

        /// <summary>
        /// 	Returns the application directory.
        /// </summary>
        /// <returns>Application Directory.</returns>
        private static string GetExecutingDirectory()
        {
            string replaceText = "file://";
            if(ExecutingEnvironment.LocalPlatform.OsPlatform == OsPlatformType.Windows) {
                replaceText = "file:///";
            }

            string codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase.Replace(replaceText, "");
            return Path.GetDirectoryName(codeBase);
        }
    }
}