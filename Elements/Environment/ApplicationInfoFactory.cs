using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Structum.Elements.Environment
{
    /// <summary>
    ///     Defines the Application Info Factory class.
    /// </summary>
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