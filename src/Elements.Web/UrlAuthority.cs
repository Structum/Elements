using System;
using System.Text.RegularExpressions;

namespace Structum.Elements.Web
{
    /// <summary>
    ///     Defines the URL Authority class.
    /// </summary>
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
        /// <exception cref="ArgumentNullException">fullAuthority</exception>
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