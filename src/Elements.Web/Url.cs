using System;
using System.Text.RegularExpressions;

namespace Structum.Elements.Web
{
    /// <summary>
    ///     Defines the URL class.
    /// </summary>
    public sealed class Url
    {
        /// <summary>
        ///     URL Regex pattern.
        /// </summary>
        private readonly Regex _urlPattern = new Regex(@"^(?<s1>(?<s0>[^:/\?#]+):)?(?<a1>//(?<a0>[^/\?#]*))?(?<p0>[^\?#]*)(?<q1>\?(?<q0>[^#]*))?(?<f1>#(?<f0>.*))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        ///     Full URL. e.g. https://aespinoza:myPassword@www.mydomain.com:8080/myfolder/otherfolder/x.aspx?id=50#some-fragment
        /// </summary>
        public readonly string FullUrl;

        /// <summary>
        ///     Scheme. e.g. https.
        /// </summary>
        public readonly string Scheme;

        /// <summary>
        ///     Authority. e.g. aespinoza:myPassword@www.mydomain.com:8080
        ///     The Authority is comprised of:
        ///     <list type="bullet">
        ///         <item>user information e.g. aespinoza:myPassword</item>
        ///         <item>host e.g. www.mydomain.com</item>
        ///         <item>port e.g. 8080</item>
        ///     </list>
        /// </summary>
        public readonly UrlAuthority Authority;

        /// <summary>
        ///     Path. e.g. /myfolder/otherfolder/x.aspx
        /// </summary>
        public readonly string Path;

        /// <summary>
        ///     Query. e.g. id=50
        /// </summary>
        public readonly string Query;

        /// <summary>
        ///     Fragment. e.g. some-fragment
        /// </summary>
        public readonly string Fragment;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        /// <param name="fullUrl">Full URL.</param>
        public Url(string fullUrl)
        {
            if (string.IsNullOrEmpty(fullUrl)) {
                throw new ArgumentNullException(nameof(fullUrl));
            }

            this.FullUrl = fullUrl;

            Match m = this._urlPattern.Match(fullUrl);

            this.Scheme = m.Groups["s0"].Value;
            this.Authority = new UrlAuthority(m.Groups["a0"].Value);
            this.Path = m.Groups["p0"].Value;
            this.Query = m.Groups["q0"].Value;
            this.Fragment = m.Groups["f0"].Value;
        }

        /// <summary>
        ///     Returns the string representation of the object.
        /// </summary>
        /// <returns>Full URL.</returns>
        public override string ToString()
        {
            return this.FullUrl;
        }
    }
}