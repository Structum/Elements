using System;
using System.Text.RegularExpressions;

namespace Structum.Elements.Web
{
    /// <summary>
    ///     Defines the URL Authority class.
    /// </summary>
    /// <example>
    ///     The following code demonstrate how to use the Url Authority class:
    ///     <code>
    ///     UrlAuthority authority = new UrlAuthority("username:password@www.mydomain.com:8080");
    ///     Console.WriteLine("Full Authority : " + authority.FullAuthority); // aespinoza:myPassword@www.mydomain.com:8080
    ///     Console.WriteLine("Authority User Info: " + authority.UserInfo);  // aespinoza:myPassword
    ///     Console.WriteLine("Authority Host: " + authority.Host);           // www.mydomain.com
    ///     Console.WriteLine("Authority Port:" + authority.Port);            // 8080
    ///     </code>
    /// </example>
    public class UrlAuthority
    {
        /// <summary>
        ///     URL Authority Regex pattern.
        /// </summary>
        private readonly Regex _urlAuthorityPattern = new Regex(@"^((?<u>.*?)@)?(?<h>.*?)(:(?<p>.[0-9]*))?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        ///     Full Authority. e.g. username:password@www.mydomain.com:8080
        /// </summary>
        public readonly string FullAuthority;

        /// <summary>
        ///     User Information. e.g. username:password
        /// </summary>
        public readonly string UserInfo;

        /// <summary>
        ///     Host. e.g. www.domain.com
        /// </summary>
        public readonly string Host;

        /// <summary>
        ///     Port. e.g. 8080.
        /// </summary>
        public readonly string Port;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UrlAuthority"/> class.
        /// </summary>
        /// <param name="fullAuthority">Full authority.</param>
        /// <exception cref="T:System.ArgumentNullException">Thrown when <paramref name="fullAuthority"/> is <c>null</c> or empty.</exception>
        public UrlAuthority(string fullAuthority)
        {
            if (string.IsNullOrEmpty(fullAuthority)) {
                throw new ArgumentNullException(nameof(fullAuthority));
            }

            this.FullAuthority = fullAuthority;

            Match m = this._urlAuthorityPattern.Match(fullAuthority);

            this.UserInfo = m.Groups["u"].Value;
            this.Host = m.Groups["h"].Value;
            this.Port = m.Groups["p"].Value;
        }

        /// <summary>
        ///     Returns the string representation of the object.
        /// </summary>
        /// <returns>Full URL Authority.</returns>
        public override string ToString()
        {
            return this.FullAuthority;
        }
    }
}