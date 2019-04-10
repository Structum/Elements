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
            string executingDirectory = GetExecutingDirectory(asm);
            return new ApplicationInfo {
                Name = name,
                Version = asm.GetName().Version.ToString(),
                ExecutingDirectory =  executingDirectory,
                CommandArguments = System.Environment.GetCommandLineArgs(),
                CompanyName = FileVersionInfo.GetVersionInfo(asm.Location).CompanyName,
                FilePath = Path.Combine(executingDirectory, name ?? "")
            };
        }

        /// <summary>
        /// 	Returns the application directory.
        /// </summary>
        /// <param name="asm">Assembly to the Executing Directory from.</param>
        /// <returns>Application Directory.</returns>
        private static string GetExecutingDirectory(Assembly asm)
        {
            string replaceText = "file://";
            if(ExecutingEnvironment.LocalPlatform.OsPlatform == OsPlatformType.Windows) {
                replaceText = "file:///";
            }

            string codeBase = asm.GetName().CodeBase.Replace(replaceText, "");
            return Path.GetDirectoryName(codeBase);
        }
    }
}