using System.Security;

namespace Structum.Elements.Extensions
{
    /// <summary>
    ///     Provides Secure String Extensions.
    /// </summary>
    public static class SecureStringExtensions
    {
        /// <summary>
        ///     Creates a Secure String from a String.
        /// </summary>
        /// <param name="secStr">Secure String Object.</param>
        /// <param name="str">String content.</param>
        /// <param name="readOnly">True to set the Make it read only, false otherwise.</param>
        /// <returns>Secure String.</returns>
        public static SecureString FromString(this SecureString secStr, string str, bool readOnly=false)
        {
            secStr = new SecureString();
            foreach (char c in str) {
                secStr.AppendChar(c);
            }

            if (readOnly) {
                secStr.MakeReadOnly();
            }

            return secStr;
        }
    }
}
