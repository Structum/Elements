using System;
using System.Text.RegularExpressions;

namespace Structum.Elements.Web
{
    /// <summary>
    ///     Defines the URL class which provides methods for parsing an URL.
    /// </summary>
    /// <example>
    ///     The following code demonstrates how to use the URL class to parse a URL:
    ///     <code>
    ///     Url url = new Url("https://aespinoza:myPassword@www.mydomain.com:8080/myfolder/otherfolder/x.aspx?id=50#some-fragment");
    /// 
    ///     Console.WriteLine("Full URL: " + url.FullUrl);                        // https://aespinoza:myPassword@www.mydomain.com:8080/myfolder/otherfolder/x.aspx?id=50#some-fragment
    ///     Console.WriteLine("Scheme: " + url.Scheme);                           // https
    ///     Console.WriteLine("Path: " + url.Path);                               // /myfolder/otherfolder/x.aspx
    ///     Console.WriteLine("Query: " + url.Query);                             // id=50
    ///     Console.WriteLine("Fragment: " + url.Fragment);                       // some-fragment
    ///     Console.WriteLine("Full Authority : " + url.Authority.FullAuthority); // aespinoza:myPassword@www.mydomain.com:8080
    ///     Console.WriteLine("Authority User Info: " + url.Authority.UserInfo);  // aespinoza:myPassword
    ///     Console.WriteLine("Authority Host: " + url.Authority.Host);           // www.mydomain.com
    ///     Console.WriteLine("Authority Port:" + url.Authority.Port);            // 8080
    ///     </code>
    /// </example>
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
        /// <exception cref="T:ArgumentNullException">Thrown when <paramref name="fullUrl"/> is null or empty.</exception>
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