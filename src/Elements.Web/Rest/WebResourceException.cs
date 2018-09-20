using System;

namespace Structum.Elements.Web.Rest
{
    /// <summary>
    ///     Defines the Web Resource Exception.
    /// </summary>
    public class WebResourceException : Exception
    {
        /// <summary>
		///     Initializes a new instance of the <see cref="WebResourceException"/> class.
        /// </summary>
        /// <param name="errors">Errors.</param>
        public WebResourceException(string errors) : base(errors) {}
    }
}

